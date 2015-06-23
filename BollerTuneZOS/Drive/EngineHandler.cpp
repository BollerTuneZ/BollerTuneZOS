// 
// 
// 

#include "EngineHandler.h"
#include "Board.h"
#include "Constants.h"
void EngineHandlerClass::init()
{
	pinMode(PIN_DIRECTION_BACKWARDS, OUTPUT);
	pinMode(PIN_DIRECTION_FORWARD, OUTPUT);
	pinMode(PIN_POWER, OUTPUT);
}

void EngineHandlerClass::Drive()
{
	if (_direction == DIRECTION_STATE_FORWARD)
	{
		digitalWrite(PIN_DIRECTION_BACKWARDS, LOW);
		digitalWrite(PIN_DIRECTION_FORWARD, HIGH);
	}
	else if (_direction == DIRECTION_STATE_BACKWARDS)
	{
		digitalWrite(PIN_DIRECTION_FORWARD, LOW);
		digitalWrite(PIN_DIRECTION_BACKWARDS, HIGH);
	}
	else
	{
		digitalWrite(PIN_DIRECTION_FORWARD, LOW);
		digitalWrite(PIN_DIRECTION_BACKWARDS, LOW);
	}
	digitalWrite(PIN_POWER, _speed);
}

int EngineHandlerClass::GetSpeed()
{
	return _speed;
}

void EngineHandlerClass::SetDirection(byte direction)
{
	_direction = direction;
}

void EngineHandlerClass::SetSpeed(int speed)
{
	_speed = speed;
}

EngineHandlerClass EngineHandler;

