// Log.h

#ifndef _LOG_h
#define _LOG_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif
#include <SD.h>
#include <SPI.h>
class LogClass
{
private:
	String FileName = "Test.log";
 protected:

	 File _logFile;
 public:
	void init();

	void Log(String text);
	void Log(char *text);

	void CloseLog();
};

extern LogClass Log;

#endif

