/*
 Name:		EncoderServer.ino
 Created:	7/28/2015 12:23:15 PM
 Author:	Developer
*/

// the setup function runs once when you press reset or power the board
#include <ArduinoJson.h>

#include <EthernetUdp.h>
#include <EthernetServer.h>
#include <EthernetClient.h>
#include <Ethernet.h>
#include <Dns.h>
#include <Dhcp.h>
#include <SPI.h>
#define ENCODER_OPTIMIZE_INTERRUPTS
#include <Encoder.h>
#include "Board.h"
#include "NetworkConfig.h"

Encoder _encoderSteering = Encoder(PIN_ENCODER_STEERING_GREEN, PIN_ENCODER_STEERING_WHITE);
Encoder _encoderMotor = Encoder(PIN_ENCODER_MOTOR_GREEN, PIN_ENCODER_MOTOR_WHITE);

EthernetServer _server(LOCAL_PORT);
EthernetClient _clients[4];
byte mac[] = {
	0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };



long ltMotor = 0;
long ltSteering = 0;


void setup() {
	Serial.begin(9600);
	Ethernet.begin(mac, LOCAL_IP, GATEWAY, SUBNET);
	delay(1000);
	_server.begin();
	Serial.println("Initialized");

}

void loop() {
	EthernetClient client = _server.available();

	if (client) {

		Serial.println("Client connected");

		boolean newClient = true;
		for (byte i = 0; i < 4; i++) {
			//check whether this client refers to the same socket as one of the existing instances:
			if (_clients[i] == client) {
				newClient = false;
				break;
			}
		}

		if (newClient) {
			Serial.println("New Client");
			//check which of the existing clients can be overridden:
			for (byte i = 0; i<4; i++) {
				if (!_clients[i] && _clients[i] != client) {
					_clients[i] = client;
					// clead out the input buffer:
					client.flush();
					break;
				}
			}
		}
	}

	for (byte i = 0; i < 4; i++)
	{
		if (_clients[i])
		{
			GetCommands(&_clients[i]);

		}
	}
	SendEncoderPositions();
}

void GetCommands(EthernetClient *client)
{
	char buffer[200];
	char counter = 0;
	while (client->available() > 0)
	{
		
		buffer[counter] = client->read();
		Serial.print(buffer[counter]);
		if (counter == 199)
		{
			break;
		}
		counter++;
	}
	if (counter == 0)
	{
		return;
	}
	Serial.println("Get Command");
	Serial.println(buffer);
	StaticJsonBuffer<200> jsonCommmandBuffer;
	JsonObject& root = jsonCommmandBuffer.parseObject(buffer);

	if (!root.success()) {
		Serial.println("Error Json");
		return;
	}
	Serial.println("Object parsed");

	const char* command = root["Command"].asString();
	String str = String(command);
	if (str == COMMAND_IDENTITY)
	{
		SendIdentity(client);
	}
	else if (str == COMMAND_SET_ENCODER)
	{
		CommandSetEncoder(root);
	}
	else
	{
		Serial.println("command was not expected:" + str);
	}
}

void CommandSetEncoder(JsonObject& object)
{
	Serial.println("Processing Set command");
	int value = object["Value"];
	const char* tempMode = object["ECMODE"];
	String ecMode = String(tempMode);

	if (ecMode == CVALUE_EC_MOTOR)
	{
		_encoderMotor.write(value);
	}
	else if (ecMode == CVALUE_EC_STEERING)
	{
		_encoderSteering.write(value);
	}
	else
	{
		Serial.println("Encoder Mode unclear:" + ecMode);
	}
}

void SendEncoderPositions()
{

	int currentMotor = _encoderMotor.read();
	int currentSteering = _encoderSteering.read();

	if (currentMotor == ltMotor && currentSteering == ltSteering)
	{
		return;
	}
	ltMotor = currentMotor;
	ltSteering = currentSteering;

	StaticJsonBuffer<200> jsonBufferAnswer;
	JsonObject& root = jsonBufferAnswer.createObject();

	root["EncoderSteering"] = currentSteering;
	root["EncoderMotor"] = currentMotor;

	char buffer[200];
	root.printTo(buffer, 200);

	for (char i = 0; i < 4; i++)
	{
		if (_clients[i])
		{
			_clients[i].println(buffer);
		}
	}
}

void SendIdentity(EthernetClient *client)
{
	Serial.println("Send Identity");
	StaticJsonBuffer<100> jsonBufferIdentitySend;
	JsonObject& data = jsonBufferIdentitySend.createObject();
	data["Name"] = IDENTITY;
	char dataBuffer[100];
	data.printTo(dataBuffer, 100);
	client->println(dataBuffer);
}