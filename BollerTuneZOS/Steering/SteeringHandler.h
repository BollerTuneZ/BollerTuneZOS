// SteeringHandler.h

#ifndef _STEERINGHANDLER_h
#define _STEERINGHANDLER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif
#include <Encoder.h>
#include "Board.h"
class SteeringHandlerClass
{

 protected:
	 Encoder motorEncoder = Encoder(PIN_ENCODER_MOTOR_GREEN, PIN_ENCODER_MOTOR_WHITE);
	 Encoder steeringEncoder = Encoder(PIN_ENCODER_STEERING_GREEN, PIN_ENCODER_STEERING_WHITE);

 public:
	void init();
	int PositionMotor;
	int PositionSteering;

	void Refresh();

	void SetMotorPosition(int position);
	void SetSteeringPosition(int position);

};

extern SteeringHandlerClass SteeringHandler;

#endif

