// MotorHandler.h

#ifndef _MOTORHANDLER_h
#define _MOTORHANDLER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class MotorHandlerClass
{
 protected:
	 int _speed;
	 byte _direction;
 public:
	void init();

	void Steer();

	void SetSpeed(int speed);

	void SetDirection(byte direction);

};

extern MotorHandlerClass MotorHandler;

#endif

