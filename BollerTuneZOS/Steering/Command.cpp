// 
// 
// 

#include "Command.h"

void CommandClass::init(){

}

void CommandClass::init(byte command, byte subcommand){
	Command = command;
	SubCommand = subcommand;
	isBadCommand = 'f';
}
void CommandClass::init(byte command, byte subcommand, byte *value)
{
	init(command, subcommand);
	Value = value;
}


CommandClass Command;

