// EngineHandler.h

#ifndef _ENGINEHANDLER_h
#define _ENGINEHANDLER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class EngineHandlerClass
{
 protected:
	 int _speed;
	 byte _direction;

 public:
	void init();

	void Drive();

	void SetSpeed(int speed);

	void SetDirection(byte direction);

	int GetSpeed();
};

extern EngineHandlerClass EngineHandler;

#endif

