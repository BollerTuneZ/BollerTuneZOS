/*
 Name:		ArduinoJsonClient.ino
 Created:	7/24/2015 9:29:04 PM
 Author:	Developer
*/

// the setup function runs once when you press reset or power the board
#include <ArduinoJson.h>
#include <SPI.h>
#include <EthernetUdp.h>
#include <EthernetServer.h>
#include <EthernetClient.h>
#include <Ethernet.h>
#include <Dns.h>
#include <Dhcp.h>

byte mac[] = {
	0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED
};
IPAddress ip(192, 168, 1, 177);

IPAddress server(192, 168, 1, 6);

int port = 55566;
long arduino_counter = 0;
EthernetClient client;

//JSON object {"name":"Jonas Ahlf","counter":1}
void setup() {
	Ethernet.begin(mac, ip);
	// Open serial communications and wait for port to open:
	Serial.begin(9600);
	while (!Serial) {
		; // wait for serial port to connect. Needed for Leonardo only
	}

	// give the Ethernet shield a second to initialize:
	delay(1000);
	Serial.println("connecting...");
	// if you get a connection, report back via serial:
	if (client.connect(server,port)) {
		Serial.println("connected");
	}
	else {
		// if you didn't get a connection to the server:
		Serial.println("connection failed");
	}
}

// the loop function runs over and over again until power down or reset
void loop() {
	SendAnswer();
	// if the server's disconnected, stop the client:
	if (!client.connected()) {
		Serial.println();
		Serial.println("disconnecting.");
		client.stop();
		// do nothing:
		while (true);
	}
}


void SendAnswer()
{

	StaticJsonBuffer<200> jsonBuffer;

	JsonObject& root = jsonBuffer.createObject();
	root["name"] = "arduino";
	root["counter"] = arduino_counter;
	arduino_counter++;

	//root.printTo(client);
	char buffer[200];
	root.printTo(buffer, 200);
	client.println(buffer);
}

void GetAnswer()
{
	String message = "";
	while (client.available())
	{
		char c = client.read();
		message += c;
	}

	if (message == "")
	{
		return;
	}
	

	int size = message.length();
	char messageBuffer[200];
	for (int i = 0; i < size; i++)
	{
		messageBuffer[i] = message[i];
	}
	StaticJsonBuffer<200> jsonBuffer;
	//Parsing
	JsonObject& root = jsonBuffer.parseObject(messageBuffer);
	if (!root.success()) {
		delay(2000);
		Serial.println("parseObject() failed");
		return;
	}
	const char* name = root["name"];
	long counter = root["counter"];
	Serial.print("Got Message from: ");
	Serial.print(name);
	Serial.print(" Sent counter:");
	Serial.println(counter);



}
