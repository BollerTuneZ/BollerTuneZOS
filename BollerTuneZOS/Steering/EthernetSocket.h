// EthernetSocket.h

#ifndef _ETHERNETSOCKET_h
#define _ETHERNETSOCKET_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class EthernetSocketClass
{
 protected:
	 IPAddress _ownAddress;
	 IPAddress _serverAddress;
	 int _serverPort = 55566;
	 EthernetClient _client;
 public:
	void init();

	bool Connect();

	bool SendPosition(int *steering, int *engine);
};

extern EthernetSocketClass EthernetSocket;

#endif

