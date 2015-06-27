// SerialCommunication.h

#ifndef _SERIALCOMMUNICATION_h
#define _SERIALCOMMUNICATION_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif
#include "SerialCommunicationConstants.h"
#include "Command.h"
#include "Log.h"

class SerialCommunicationClass
{
 protected:
	 LogClass _log;

 public:
	void init();

	CommandClass Receive();

	void Send(String message);
	void Send(int message);


};

extern SerialCommunicationClass SerialCommunication;

#endif

