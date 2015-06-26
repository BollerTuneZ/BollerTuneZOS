// Log.h

#ifndef _LOG_h
#define _LOG_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif
#include "Constants.h"

//Levels
#define LOG_LEVEL_DEBUG "Debug"
#define LOG_LEVEL_INFO "Info"
#define LOG_LEVEL_WARN "Warn"
#define LOG_LEVEL_ERROR "Error"

//LogConstante
#define LOG_BEGIN "LOG_"
#define CHAR_ID_OPEN '['
#define CHAR_ID_CLOSED ']'
#define CHAR_LEVEL_OPEN '{'
#define CHAR_LEVEL_CLOSED '}'

class LogClass
{
protected:
	//Pattern Log LOG_%IDENTITY% Class[id]{LEVEL} Message
	String _className;
	static int id;
public:
	void init(String classname);

	void Log(String level, String message);
};

extern LogClass Log;

#endif

