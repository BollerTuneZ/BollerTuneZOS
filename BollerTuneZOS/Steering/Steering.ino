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
LogClass _log;
// the setup function runs once when you press reset or power the board
void setup() {
	//Auswahl der Kommunikation
	_log.init("Main");
	if (COM_CURRENT == SERIAL) //Serielle Schnittstelle wird benuntz
	{
		_comSerial.init();
	}
	_log.Log(LOG_LEVEL_INFO, "Initialize Motorhandler");
	_motorHandler.init(); 
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
	String message = 
		"{EN}:"
		+ String(_steeringHandler.PositionSteering)
		+ "/"
		+ String(_steeringHandler.PositionMotor);
	_comSerial.Send(message);
}

void ExecuteCommand(CommandClass command)
{
	if (command.Command == COMMAND_DIRECTION)
	{
		_log.Log(LOG_LEVEL_DEBUG, "Set Direction to:" + String(command.SubCommand));
		_motorHandler.SetDirection(command.SubCommand);
	}
	else if (command.Command == COMMAND_POWER)
	{
		_log.Log(LOG_LEVEL_DEBUG, "Set Speed to:" + String((int)command.SubCommand));
		_motorHandler.SetSpeed(command.SubCommand);
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
