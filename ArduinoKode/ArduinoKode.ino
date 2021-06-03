#include "header.h"

ControllerPin *inputHead, *currentInput;

const int BUFFER_SIZE = 2;

byte cmd;
byte arg;
String inputPins, commandData;
bool initialSetupDone;

void setup() {
  Serial.begin(9600);
  inputHead = NULL;
  inputPins = "";
  initialSetupDone = false;
}

void loop() {
  if (!initialSetupDone) {
    if (Serial.available() > 0) {
      ReadSerial();

      switch(cmd){
        case 0x01:
          AddInputPin(arg);
          break;
        case 0x02:
          pinMode(arg, OUTPUT);
          break;
        case 0x10:
          initialSetupDone = true;
          inputPins = inputPins + "|";
          break;
      }
    }
  } else {
    currentInput = inputHead;
    commandData = "";
    while(currentInput != NULL){
      commandData.concat(String(GetPinValue(currentInput->pin)) + ",");
      currentInput = currentInput->next;
    }

    commandData.remove(commandData.length()-1);
    Serial.println(inputPins + commandData);
    
    if (Serial.available() > 0) {
      ReadSerial();
      if (cmd == 0x20){
        PORTA = arg;
      }
    }
  }
}

void AddInputPin(byte pin){
  ControllerPin *newPin = (ControllerPin*)malloc(sizeof(ControllerPin));
  newPin->pin = pin;
  newPin->next = NULL;

  pinMode(newPin->pin, INPUT);

  if(inputHead == NULL){
    inputHead = newPin;
    inputPins.concat(String(pin));
  } else {
    ControllerPin *current = inputHead;
    while(current->next != NULL){
      current = current->next;
    }
    current->next = newPin;
    inputPins.concat("," + String(pin, DEC));
  }
}

int GetPinValue(byte pin){
  if(pin >= numberOfDigitalPorts){
    return analogRead(pin);
  }
  return digitalRead(pin);
}

void ReadSerial(){
  char buf[BUFFER_SIZE];
  Serial.readBytes(buf, BUFFER_SIZE);
  cmd = buf[0];
  arg = buf[1];
}
