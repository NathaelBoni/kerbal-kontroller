#ifndef _pin_helper_h
#define _pin_helper_h

#include "KerbalSimpit.h"
#include "Definitions.h"

extern unsigned long lastDebounce[ButtonsToDebounce];
extern byte currentState[ButtonsToDebounce], lastState[ButtonsToDebounce];
extern byte pins[ButtonsToDebounce];

void InitializePinsArray();
void SetPinMode();
void ReadButton(int pinIndex, void (*callback)());
void ReadSwitch(int pinIndex, void (*callback)());
void SetSASLed(byte sasMode);

#endif
