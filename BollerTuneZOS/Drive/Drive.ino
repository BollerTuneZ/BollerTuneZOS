/*
 Name:		Drive.ino
 Created:	6/20/2015 1:57:04 AM
 Author:	Developer
*/

// the setup function runs once when you press reset or power the board
#include "EngineHandler.h"
#include "SerialCommunication.h"
#include "Command.h"
#include "Board.h"
#include "Config.h"

SerialCommunicationClass _comSerial;
EngineHandlerClass _engineHandler;
void setup() {
	if (COM_CURRENT == SERIAL) //Serielle Schnittstelle wird benuntz
	{
		_comSerial.init();
	}
	_engineHandler.init();
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
	_engineHandler.Drive();
}

void ExecuteCommand(CommandClass command)
{
	Serial.println("Execute Command");
	if (command.Command == COMMAND_DIRECTION)
	{
		Serial.print("Direction set to:");
		Serial.println(command.SubCommand);
		_engineHandler.SetDirection(command.SubCommand);
	}
	else if (command.Command == COMMAND_DRIVE_POWER)
	{
		Serial.print("Speed set to:");
		Serial.println(command.SubCommand);
		_engineHandler.SetSpeed(command.SubCommand);
	}
	else if (command.Command == COMMAND_DRIVE_READ)
	{
		_comSerial.Send(_engineHandler.GetSpeed());
	}
}
