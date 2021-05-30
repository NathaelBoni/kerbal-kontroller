#define numberOfDigitalPorts 54

typedef struct ControllerPin {
  byte pin;
  ControllerPin *next;
} ControllerPin;

void AddInputPin(byte pin);
void AddSASLeds(byte pin);
void TurnSASLed(byte pin);
int GetPinValue(byte pin);
void ReadSerial();
