// 
// 
// 

#include "MotorHandler.h"
#include "Board.h"
#include "Config.h"
void MotorHandlerClass::init()
{
	pinMode(PIN_DIRECTION_LEFT, OUTPUT);
	pinMode(PIN_DIRECTION_RIGHT, OUTPUT);
	pinMode(PIN_DRIVE, OUTPUT);
}

void MotorHandlerClass::Steer()
{
	if (_direction == DIRECTION_STATE_LEFT)
	{
		digitalWrite(PIN_DIRECTION_RIGHT, LOW);
		digitalWrite(PIN_DIRECTION_LEFT, HIGH);
	}
	else if (_direction == DIRECTION_STATE_RIGHT)
	{
		digitalWrite(PIN_DIRECTION_LEFT, LOW);
		digitalWrite(PIN_DIRECTION_RIGHT, HIGH);
	}
	else
	{
		digitalWrite(PIN_DIRECTION_LEFT, LOW);
		digitalWrite(PIN_DIRECTION_RIGHT, LOW);
	}

	if (_speed < MIN_SPEED)
	{
		digitalWrite(PIN_DRIVE, IDLE_SPEED);
	}
	else if (_speed > MAX_SPEED)
	{
		digitalWrite(PIN_DRIVE, MAX_SPEED);
	}
	else
	{
		digitalWrite(PIN_DRIVE, _speed);
	}
}

void MotorHandlerClass::SetSpeed(int speed)
{
	_speed = speed;
}

void MotorHandlerClass::SetDirection(byte direction)
{
	_direction = direction;
}
MotorHandlerClass MotorHandler;

