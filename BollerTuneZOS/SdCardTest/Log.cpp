// 
// 
// 

#include "Log.h"

void LogClass::init()
{
	
	if (!SD.begin(4)) {
		while (true)
		{
			Serial.println("initialization failed!");
		}
		return;
	}
	Serial.println("initialization done.");
	_logFile = SD.open(FileName, FILE_WRITE);
	if (!_logFile)
	{
		Serial.println("ERROR_SDCARD");
	}

}

void LogClass::Log(String text)
{
	_logFile.println(text);
}
void LogClass::Log(char *text)
{
	_logFile.println(text);
}

void LogClass::CloseLog()
{
	_logFile.close();
}

LogClass Log;

