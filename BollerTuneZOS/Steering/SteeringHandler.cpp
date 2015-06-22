// 
// 
// 

#include "SteeringHandler.h"
#include "Board.h"
void SteeringHandlerClass::init()
{

}

void SteeringHandlerClass::Refresh()
{
	PositionMotor = motorEncoder.read();
	PositionSteering = steeringEncoder.read();
}

void SteeringHandlerClass::SetMotorPosition(int position)
{
	motorEncoder.write(position);
}

void SteeringHandlerClass::SetSteeringPosition(int position)
{
	steeringEncoder.write(position);
}

SteeringHandlerClass SteeringHandler;

