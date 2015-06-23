// SerialCommunication.h

#ifndef _SERIALCOMMUNICATION_h
#define _SERIALCOMMUNICATION_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif
#include "Constants.h"
#include "Command.h"
class SerialCommunicationClass
{
protected:


public:
	void init();

	CommandClass Receive();

	void Send(String message);
	void Send(int message);


};

extern SerialCommunicationClass SerialCommunication;

#endif

