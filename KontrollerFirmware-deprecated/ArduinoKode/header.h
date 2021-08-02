#define numberOfDigitalPorts 54

typedef struct ControllerPin {
  byte pin;
  ControllerPin *next;
} ControllerPin;

void AddInputPin(byte pin);
int GetPinValue(byte pin);
void ReadSerial();
