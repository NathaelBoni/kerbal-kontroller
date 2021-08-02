#include <Arduino.h>
#include "KerbalSimpit.h"
#include "PinHelper.h"

unsigned long lastDebounce[ButtonsToDebounce];
byte currentState[ButtonsToDebounce], lastState[ButtonsToDebounce];
byte pins[ButtonsToDebounce];

void ReadInput(int pinIndex, boolean isSwitch, void (*callback)()){
    int reading = digitalRead(pins[pinIndex]);
  if (reading != lastState[pinIndex]) {
    lastDebounce[pinIndex] = millis();
  }

  if ((millis() - lastDebounce[pinIndex]) > DebounceDelay) {
    if (reading != currentState[pinIndex]) {
      currentState[pinIndex] = reading;
      if (currentState[pinIndex] == HIGH || isSwitch) {
        callback();
      }
    }
  }
  lastState[pinIndex] = reading;
}

void ReadButton(int pinIndex, void (*callback)()) {
  ReadInput(pinIndex, false, callback);
}

void ReadSwitch(int pinIndex, void (*callback)()) {
  ReadInput(pinIndex, true, callback);
}

void SetPinMode() {
  pinMode(LeftJoyStickX, INPUT);
  pinMode(LeftJoyStickY, INPUT);
  pinMode(ExtraLeftJoyStickX, INPUT);
  pinMode(RightJoyStickX, INPUT);
  pinMode(RightJoyStickY, INPUT);
  pinMode(ExtraRightJoyStickY, INPUT);
  pinMode(AnalogThrottle, INPUT);

  for (int i = 0; i < ButtonsToDebounce; i++) {
    pinMode(pins[i], INPUT);
  }

  pinMode(SASLedS0, OUTPUT);
  pinMode(SASLedS1, OUTPUT);
  pinMode(SASLedS2, OUTPUT);
  pinMode(SASLedS3, OUTPUT);
}

void SetSASLed(byte sasMode) {
  PORTA = (PORTA & 0xF0) | sasMode;
}

void InitializePinsArray() {
  pins[0] = PrecisionLeftButton;
  pins[1] = PrecisionRightButton;
  pins[2] = StageButton;
  pins[3] = AbortButton;
  pins[4] = LandingGearSwitch;
  pins[5] = BrakesSwitch;
  pins[6] = LightsSwitch;
  pins[7] = SASSwitch;
  pins[8] = RCSSwitch;
  pins[9] = KeyQ;
  pins[10] = KeyW;
  pins[11] = KeyE;
  pins[12] = KeyA;
  pins[13] = KeyS;
  pins[14] = KeyD;
  pins[15] = KeyC;
  pins[16] = KeyF;
  pins[17] = KeyB;
  pins[18] = KeyR;
  pins[19] = KeyP;
  pins[20] = KeyI;
  pins[21] = Action1Button;
  pins[22] = Action2Button;
  pins[23] = Action3Button;
  pins[24] = Action4Button;
  pins[25] = Action5Button;
  pins[26] = SASFreeButton;
  pins[27] = SASManeuverButton;
  pins[28] = SASProgradeButton;
  pins[29] = SASRetrogradeButton;
  pins[30] = SASRadialInButton;
  pins[31] = SASRadialOutButton;
  pins[32] = SASNormalButton;
  pins[33] = SASAntiNormalButton;
  pins[34] = SASTargetButton;
  pins[35] = SASAntiTargetButton;
  pins[36] = DecreaseTimeWarpButton;
  pins[37] = IncreaseTimeWarpButton;
  pins[38] = PreviousVesselButton;
  pins[39] = NextVesselButton;
  pins[40] = CameraCycleButton;
  pins[41] = OrbitalViewButton;
  pins[42] = QuickSaveButton;
  pins[43] = QuickLoadButton;
  pins[44] = PauseButton;
}
