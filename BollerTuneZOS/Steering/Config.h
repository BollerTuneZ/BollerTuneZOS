#ifndef CONFIG_h
#define CONFIG_h

//Motor einstellungen
#define IDLE_SPEED 0 //Geschwindigkeit die genommen wird wenn der ggb. wert unter dem minium liegt
#define MIN_SPEED 0 //Anfahrst geschwindigkeit
#define MAX_SPEED 255 //Maximale Geschwindigkeit
#define DIRECTION_STATE_LEFT 'F'
#define DIRECTION_STATE_RIGHT 'B'

//Kommunikations einstellungen
#define NETWORK "network"
#define SERIAL "serial"
#define COM_DEFAULT SERIAL

#define COM_CURRENT COM_DEFAULT

#define IDENTITY "Steering"

#endif