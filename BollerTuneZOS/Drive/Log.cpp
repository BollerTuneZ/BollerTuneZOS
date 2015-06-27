// 
// 
// 

#include "Log.h"

void LogClass::init(String classname)
{
	_className = classname;
}

void LogClass::Log(String level, String message)
{
	//Pattern Log LOG_%IDENTITY% Class[id]{LEVEL} Message

	String printMessage = LOG_BEGIN;
	message += "DRIVE ";
	message += _className;
	message += (CHAR_ID_OPEN + String(id) + CHAR_ID_CLOSED);
	message += (CHAR_LEVEL_OPEN + level + CHAR_LEVEL_CLOSED);
	message += (" " + message);
	Serial.println(printMessage);
}

LogClass Log;

