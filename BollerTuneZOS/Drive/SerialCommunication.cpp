// 
// 
// 
/*
Pattern {Command,SubCommand,[Value])
*/
#include "SerialCommunication.h"
#include "Arduino.h"
void SerialCommunicationClass::init()
{
	pinMode(13, OUTPUT);
	Serial.begin(BOUD_RATE);
	Serial.println("Init Serial");
	byte *dataArray = new byte[2];

	while (true) //Suche nach Host
	{
		if (Serial.available() == 2)
		{
			dataArray[0] = Serial.read();
			dataArray[2] = Serial.read();

			if (dataArray[0] != START_BYTE)
			{
				digitalWrite(6, LOW);
				digitalWrite(13, LOW);
				continue;
			}
			Serial.println(IDENTITY);
			
			break;
		}
		digitalWrite(13, LOW);
	}
}

CommandClass SerialCommunicationClass::Receive()
{
	CommandClass command;
	int streamLenght = Serial.available();
	if (streamLenght < 2)
	{
		command.init();
		return command;
	}

	byte *buffer = new byte[streamLenght];
	for (int i = 0; i < sizeof(buffer); i++)
	{
		buffer[i] = Serial.read();
	}
	byte commandByte = buffer[0];
	byte subCommandByte = buffer[1];
	byte *value;
	char gotValue = 'f';
	if (streamLenght > 2)
	{
		gotValue = 't';
		int dataLength = (streamLenght - 2);
		value = new byte[dataLength];
		for (int i = 2; i < streamLenght; i++)
		{
			value[(i - 2)] = buffer[i];
		}
	}

	if (gotValue == 't')
	{
		command.init(commandByte, subCommandByte, value);
	}
	else
	{
		command.init(commandByte, subCommandByte);
	}
	return command;
}

void SerialCommunicationClass::Send(String message){
	String payload = IDENTITY;
	payload += ":" + message;
	Serial.println(payload);
}
void SerialCommunicationClass::Send(int message)
{
	String payload = IDENTITY;
	payload += ":" + String(message);
	Serial.println(payload);
}

SerialCommunicationClass SerialCommunication;

