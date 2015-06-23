/*
 Name:		SdCardTest.ino
 Created:	6/21/2015 11:56:30 AM
 Author:	Developer
*/

// the setup function runs once when you press reset or power the board

#include <SPI.h>
#include <SD.h>
#include "Log.h"
LogClass _log;
void setup() {
	Serial.begin(9600);
	pinMode(10, OUTPUT);
	_log.init();
	for (int i = 0; i < 100; i++)
	{
		String text = "Hallo" + String(i);
		Serial.println(text);
		_log.Log(text);
	}
	_log.CloseLog();
}

// the loop function runs over and over again until power down or reset
void loop() {
  
}
