/*
 Name:		Drive.ino
 Created:	6/20/2015 1:57:04 AM
 Author:	Developer
*/

// the setup function runs once when you press reset or power the board
#include "Log.h"
#include "EngineHandler.h"
#include "SerialCommunication.h"
#include "Command.h"
#include "Board.h"
#include "Config.h"

SerialCommunicationClass _comSerial;
EngineHandlerClass _engineHandler;
LogClass _log;

void setup() {
	if (COM_CURRENT == SERIAL) //Serielle Schnittstelle wird benuntz
	{
		_comSerial.init();
	}
	_log.init("Main");
	_log.Log(LOG_LEVEL_INFO, "Initialize EgineHandler");
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
	if (command.Command == COMMAND_DIRECTION)
	{
		_log.Log(LOG_LEVEL_DEBUG, "Direction set to:" + String(command.SubCommand));
		_engineHandler.SetDirection(command.SubCommand);
	}
	else if (command.Command == COMMAND_DRIVE_POWER)
	{
		_log.Log(LOG_LEVEL_DEBUG, "Drivepower set to:" + String(command.SubCommand));
		_engineHandler.SetSpeed(command.SubCommand);
	}
	else if (command.Command == COMMAND_DRIVE_READ)
	{
		_comSerial.Send(_engineHandler.GetSpeed());
	}
}
