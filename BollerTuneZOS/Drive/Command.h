// Command.h

#ifndef _COMMAND_h
#define _COMMAND_h


#include "Arduino.h"


class CommandClass
{
protected:


public:

	void init();
	void init(byte command, byte subcommand);
	void init(byte command, byte subcommand, byte *value);

	char isBadCommand = 't';

	byte Command;
	byte SubCommand;
	byte *Value;
};

extern CommandClass Command;

#endif

