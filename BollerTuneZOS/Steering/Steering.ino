#include "Log.h"
#include <Encoder.h>
#include "SteeringHandler.h"
#include "MotorHandler.h"
#include "Command.h"
#include "SerialCommunication.h"
#include "Board.h"
#include "Config.h"
/*
 Name:		Steering.ino
 Created:	6/18/2015 10:42:25 PM
 Author:	Developer
*/

SerialCommunicationClass _comSerial;
MotorHandlerClass _motorHandler;
SteeringHandlerClass _steeringHandler;
unsigned long lastTimeBlink;
int blinkState = HIGH;
// the setup function runs once when you press reset or power the board
void setup() {
	//Auswahl der Kommunikation
	pinMode(5, OUTPUT);
	pinMode(6, OUTPUT);
	if (COM_CURRENT == SERIAL) //Serielle Schnittstelle wird benuntz
	{
		_comSerial.init();
	}
	_motorHandler.init(); 
	lastTimeBlink = millis();
}

// the loop function runs over and over again until power down or reset
void loop() {
	CommandClass tempCommand;
	if (COM_CURRENT == SERIAL)
	{
		tempCommand = _comSerial.Receive();
	}
	if (tempCommand.isBadCommand != 't')
	{
		ExecuteCommand(tempCommand);
	}
	_motorHandler.Steer();
	_steeringHandler.Refresh();
	SendPositionMessage();
}

void SendPositionMessage()
{
	//Message pattern {EN}:<SteeringPosition>/<MotorPosition>
	Blink(5);
	String message = 
		"{EN}:"
		+ String(_steeringHandler.PositionSteering)
		+ "/"
		+ String(_steeringHandler.PositionMotor);
	_comSerial.Send(message);
}

void ExecuteCommand(CommandClass command)
{
	Serial.println("Execute Command");
	if (command.Command == COMMAND_DIRECTION)
	{
		Serial.print("Direction set to:");
		Serial.println(command.SubCommand);
		_motorHandler.SetDirection(command.SubCommand);
	}
	else if (command.Command == COMMAND_POWER)
	{
		Serial.print("Speed set to:");
		Serial.println(command.SubCommand);
		_motorHandler.SetSpeed(command.SubCommand);
		Blink(6);
	}
	else if (command.Command == COMMAND_SET_ENCODER)
	{
		String strValue = String(*command.Value);

		int value = strValue.toInt();
		
		byte subCommand = command.SubCommand;

		if (subCommand == ENCODER_MOTOR)
		{
			_steeringHandler.SetMotorPosition(value);
		}
		if (subCommand == ENCODER_STEERING)
		{
			_steeringHandler.SetSteeringPosition(value);
		}
		_steeringHandler.Refresh();
	}
}
void Blink(int pin)
{
	unsigned long current = millis();

	unsigned long diff = current - lastTimeBlink;
	if (diff >= 500)
	{
		if (blinkState == HIGH)
		{
			blinkState = LOW;
		}
		else
		{
			blinkState = HIGH;
		}
		lastTimeBlink = current;
	}
	digitalWrite(pin, blinkState);
}
