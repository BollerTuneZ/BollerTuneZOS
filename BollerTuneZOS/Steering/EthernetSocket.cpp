// 
// 
// 

#include "EthernetSocket.h"

void EthernetSocketClass::init()
{
	_ownAddress = IPAddress(192, 168, 1, 170);
	_serverAddress = IPAddress(192, 168, 1, 6);
	


}
bool EthernetSocketClass::Connect(){
	byte mac[] = {
		0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED
	};
	Ethernet.begin(mac, _ownAddress);

	delay(1000);

	if (_client.connect(_serverAddress, _serverPort)) {
		return true;
	}
	else
	{
		return false;
	}
}

bool EthernetSocketClass::SendPosition(int *steering, int *engine)
{

}

EthernetSocketClass EthernetSocket;

