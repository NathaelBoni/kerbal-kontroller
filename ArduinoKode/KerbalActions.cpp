#include <Arduino.h>
#include "KerbalSimpit.h"
#include "KerbalActions.h"
#include "Definitions.h"

KerbalSimpit simpit(Serial);

timewarpMessage timewarp;

bool leftPrecisionEnabled, rightPrecisionEnabled;
int leftMaxValue, leftMinValue;
int rightMaxValue, rightMinValue;

GameStatus gameStatus;
FlightType flightType;
byte currentSASMode, lastSASMode;
WalkingDirectionX currentXDirection, lastXDirection;
WalkingDirectionY currentYDirection, lastYDirection;
WalkingDirectionZ currentZDirection, lastZDirection;
WalkingDirectionRoll currentRollDirection, lastRollDirection;

void SetFlightType(FlightType ft) {
  if (gameStatus == Flight) {
    flightType = ft;
  }
}

void UpdateVesselActions() {
  boolean sas = digitalRead(SASSwitch) == HIGH;
  boolean rcs = digitalRead(RCSSwitch) == HIGH;
  boolean landing = digitalRead(LandingGearSwitch) == HIGH;
  boolean lights = digitalRead(LightsSwitch) == HIGH;
  boolean breaks = digitalRead(BrakesSwitch) == HIGH;

  if (sas) {
    simpit.activateAction(SAS_ACTION);
  } else {
    simpit.deactivateAction(SAS_ACTION);
  }
  if (rcs) {
    simpit.activateAction(RCS_ACTION);
  } else {
    simpit.deactivateAction(RCS_ACTION);
  }
  if (landing) {
    simpit.activateAction(GEAR_ACTION);
  } else {
    simpit.deactivateAction(GEAR_ACTION);
  }
  if (lights) {
    simpit.activateAction(LIGHT_ACTION);
  } else {
    simpit.deactivateAction(LIGHT_ACTION);
  }
  if (breaks) {
    simpit.activateAction(BRAKES_ACTION);
  } else {
    simpit.deactivateAction(BRAKES_ACTION);
  }
}

void InitializeActions() {
  leftMaxValue = MaxOutput;
  leftMinValue = MinOutput;
  rightMaxValue = MaxOutput;
  rightMinValue = MinOutput;

  Serial.begin(115200);

  while (!simpit.init()) {
    delay(100);
  }

  simpit.registerChannel(FLIGHT_STATUS_MESSAGE);
  simpit.registerChannel(SAS_MODE_INFO_MESSAGE);
  simpit.inboundHandler(CallbackHandler);
}

void PressKey(byte key, byte modifier = 0x00){
  keyboardEmulatorMessage message = keyboardEmulatorMessage(key);
  if(modifier != 0x00){
    message.modifier = modifier;
  }
  simpit.send(KEYBOARD_EMULATOR, message);
}

void PressQ() {
  if(gameStatus == EVA){
    PressKey(InputQ);
  }
}

void PressW() {
  if(gameStatus == EVA){
    PressKey(InputW);
  }
}

void PressE() {
  if(gameStatus == EVA){
    PressKey(InputE);
  }
}

void PressA() {
  if(gameStatus == EVA){
    PressKey(InputA);
  }
}

void PressS() {
  if(gameStatus == EVA){
    PressKey(InputS);
  }
  else{
    SetFlightType(SpaceShip);
  }
}

void PressD() {
  if(gameStatus == EVA){
    PressKey(InputD);
  }
}

void PressC() {
  if(gameStatus == EVA){
    PressKey(InputSpace);
  }
}

void PressF() {
  if(gameStatus == EVA){
    PressKey(InputF);
  }
}

void PressB() {
  if(gameStatus == EVA){
    PressKey(InputB);
  }
}

void PressR() {
  if(gameStatus == EVA){
    PressKey(InputR);
  }else{
    SetFlightType(Rover);
  }
}

void PressP() {
  if(gameStatus == EVA){
    PressKey(InputP);
  }else{
    SetFlightType(Plane);
  }
}

void PressI() {
  if(gameStatus == EVA){
    PressKey(InputI);
  }
}

void PressShift() {
  PressKey(InputShift);
}

void PressCtrl() {
  PressKey(InputCtrl);
}

void Press1() {
  PressKey(Input1);
}

void Press2() {
  PressKey(Input2);
}

void Press3() {
  PressKey(Input3);
}

void Press4() {
  PressKey(Input4);
}

void Press5() {
  PressKey(Input5);
}

void ToggleLandingGear() {
  simpit.toggleAction(GEAR_ACTION);
  UpdateVesselActions();
}

void ToggleBrakes() {
  simpit.toggleAction(BRAKES_ACTION);
  UpdateVesselActions();
}

void ToggleLights() {
  simpit.toggleAction(LIGHT_ACTION);
  UpdateVesselActions();
}

void ToggleSAS() {
  simpit.toggleAction(SAS_ACTION);
  UpdateVesselActions();
}

void ToggleRCS() {
  if (gameStatus == Flight){
    simpit.toggleAction(RCS_ACTION);
    UpdateVesselActions();
  }else{
    PressR();
  }
}

void Abort() {
  simpit.activateAction(STAGE_ACTION);
}

void Stage() {
  simpit.activateAction(ABORT_ACTION);
}

void SetSASFree() {
  simpit.setSASMode(AP_STABILITYASSIST);
}

void SetSASManeuver() {
  simpit.setSASMode(AP_MANEUVER);
}

void SetSASPrograde() {
  simpit.setSASMode(AP_PROGRADE);
}

void SetSASRetrograde() {
  simpit.setSASMode(AP_RETROGRADE);
}

void SetSASRadialIn() {
  simpit.setSASMode(AP_RADIALIN);
}

void SetSASRadialOut() {
  simpit.setSASMode(AP_RADIALOUT);
}

void SetSASNormal() {
  simpit.setSASMode(AP_NORMAL);
}

void SetSASAntiNormal() {
  simpit.setSASMode(AP_ANTINORMAL);
}

void SetSASTarget() {
  simpit.setSASMode(AP_TARGET);
}

void SetSASAntiTarget() {
  simpit.setSASMode(AP_ANTITARGET);
}

void DecreaseTimeWarp() {
  timewarp.command = TIMEWARP_UP;
  simpit.send(TIMEWARP_MESSAGE, timewarp);
}

void IncreaseTimeWarp() {
  timewarp.command = TIMEWARP_DOWN;
  simpit.send(TIMEWARP_MESSAGE, timewarp);
}

void PreviousVessel() {
  PressKey(InputPlus);
  UpdateVesselActions();
}

void NextVessel() {
  PressKey(InputMinus);
  UpdateVesselActions();
}

void CameraCycle() {
  //simpit.setCameraMode(CAMERA_NEXT);
  PressKey(InputV);
}

void OrbitalView() {
  PressKey(InputM);
}

void QuickSave() {
  PressKey(InputF5);
}

void QuickLoad() {
  PressKey(InputF9, KEY_DOWN_MOD);
  delay(2000);
  PressKey(InputF9, KEY_UP_MOD);
}

void Pause() {
  PressKey(InputESC);
}

void SetLeftPrecision() {
  leftPrecisionEnabled = !leftPrecisionEnabled;
  if (leftPrecisionEnabled) {
    leftMaxValue = PrecisionMaxOutput;
    leftMinValue = PrecisionMinOutput;
  } else {
    leftMaxValue = MaxOutput;
    leftMinValue = MinOutput;
  }
}

void SetRightPrecision() {
  rightPrecisionEnabled = !rightPrecisionEnabled;
  if (rightPrecisionEnabled) {
    rightMaxValue = PrecisionMaxOutput;
    rightMinValue = PrecisionMinOutput;
  } else {
    rightMaxValue = MaxOutput;
    rightMinValue = MinOutput;
  }
}

int MapInputLeft(byte pin, bool isInverted) {
  int reading = analogRead(pin);
  if (abs(reading - 512) < Deadzone) {
    return 0;
  }

  if (isInverted){
    return map(reading, 1023, 0, leftMinValue, leftMaxValue);
  }
  return map(reading, 0, 1023, leftMinValue, leftMaxValue);
}

int MapInputRight(byte pin, bool isInverted) {
  int reading = analogRead(pin);
  if (abs(reading - 512) < Deadzone) {
    return 0;
  }

  if (isInverted){
    return map(reading, 1023, 0, rightMinValue, rightMaxValue);
  }
  return map(reading, 0, 1023, rightMinValue, rightMaxValue);
}

void ProcessRotationSpaceShip() {
  rotationMessage rotation;
  int pitch = MapInputLeft(LeftJoyStickY, false);
  int roll = MapInputLeft(ExtraLeftJoyStickX, true);
  int yaw = MapInputLeft(LeftJoyStickX, false);

  rotation.setPitchRollYaw(pitch, roll, yaw);
  simpit.send(ROTATION_MESSAGE, rotation);
}

void ProcessRotationPlane() {
  rotationMessage rotation;
  int pitch = MapInputLeft(LeftJoyStickY, false);
  int roll = MapInputLeft(LeftJoyStickX, false);
  int yaw = MapInputLeft(ExtraLeftJoyStickX, true);

  rotation.setPitchRollYaw(pitch, roll, yaw);
  simpit.send(ROTATION_MESSAGE, rotation);
}

void ProcessTranslation() {
  translationMessage translation;
  int x = MapInputRight(RightJoyStickX, true);
  int y = MapInputRight(RightJoyStickY, false);
  int z = MapInputRight(ExtraRightJoyStickY, false);

  translation.setXYZ(x, y, z);
  simpit.send(TRANSLATION_MESSAGE, translation);
}

void ProcessRoverWheels() {
  wheelMessage rover;
  int steer = MapInputLeft(LeftJoyStickX, true);
  int throttle = MapInputRight(RightJoyStickY, false);

  rover.setSteerThrottle(steer, throttle);
  simpit.send(WHEEL_MESSAGE, rover);
}

void ProcessThrottle() {
  throttleMessage throttle;
  throttle.throttle = map(constrain(analogRead(AnalogThrottle), MinThrottle, MaxThrottle), MinThrottle, MaxThrottle, 0, 32767);
  simpit.send(THROTTLE_MESSAGE, throttle);
}

WalkingDirectionX ReadXWalkingDirection(byte pin, int halfInput) {
  int reading = analogRead(pin);
  if (abs(reading - halfInput) < Deadzone) {
    return XStop;
  }
  if (reading > halfInput) {
    return Right;
  }
  return Left;
}

WalkingDirectionY ReadYWalkingDirection(byte pin, int halfInput) {
  int reading = analogRead(pin);
  if (abs(reading - halfInput) < Deadzone) {
    return YStop;
  }
  if (reading > halfInput) {
    return Forward;
  }
  return Backward;
}

WalkingDirectionZ ReadZWalkingDirection(byte pin, int halfInput) {
  int reading = analogRead(pin);
  if (abs(reading - halfInput) < Deadzone) {
    return ZStop;
  }
  if (reading > halfInput) {
    return Up;
  }
  return Down;
}

WalkingDirectionRoll ReadRollWalkingDirection(byte pin, int halfInput) {
  int reading = analogRead(pin);
  if (abs(reading - halfInput) < Deadzone) {
    return RollStop;
  }
  if (reading > halfInput) {
    return ClockWise;
  }
  return AntiClockWise;
}

void ProcessKerbalWalking() {
  currentXDirection = ReadXWalkingDirection(RightJoyStickX, HalfInput);
  currentYDirection = ReadYWalkingDirection(RightJoyStickY, HalfInput);
  currentZDirection = ReadZWalkingDirection(ExtraRightJoyStickY, HalfInput);
  currentRollDirection = ReadRollWalkingDirection(LeftJoyStickX, HalfInput);

  if (lastXDirection != currentXDirection) {
    switch (currentXDirection) {
      case Left:
        PressKey(InputA, KEY_DOWN_MOD);
        break;
      case Right:
        PressKey(InputD, KEY_DOWN_MOD);
        break;
      default:
        PressKey(InputA, KEY_UP_MOD);
        PressKey(InputD, KEY_UP_MOD);
        break;
    }
    lastXDirection = currentXDirection;
  }

  if (lastYDirection != currentYDirection) {
    switch (currentYDirection) {
      case Backward:
        PressKey(InputS, KEY_DOWN_MOD);
        break;
      case Forward:
        PressKey(InputW, KEY_DOWN_MOD);
        break;
      default:
        PressKey(InputS, KEY_UP_MOD);
        PressKey(InputW, KEY_UP_MOD);
        break;
    }
    lastYDirection = currentYDirection;
  }

  if (lastZDirection != currentZDirection) {
    switch (currentZDirection) {
      case Down:
        PressKey(InputCtrl, KEY_DOWN_MOD);
        break;
      case Up:
        PressKey(InputShift, KEY_DOWN_MOD);
        break;
      default:
        PressKey(InputCtrl, KEY_UP_MOD);
        PressKey(InputShift, KEY_UP_MOD);
        break;
    }
    lastZDirection = currentZDirection;
  }

  if (lastRollDirection != currentRollDirection) {
    switch (currentRollDirection) {
      case AntiClockWise:
        PressKey(InputQ, KEY_DOWN_MOD);
        break;
      case ClockWise:
        PressKey(InputE, KEY_DOWN_MOD);
        break;
      default:
        PressKey(InputQ, KEY_UP_MOD);
        PressKey(InputE, KEY_UP_MOD);
        break;
    }
    lastRollDirection = currentRollDirection;
  }
}

void SimpitUpdate() {
  simpit.update();
}

void CallbackHandler(byte messageType, byte message[], byte messageSize) {
  switch (messageType) {
    case FLIGHT_STATUS_MESSAGE:
      if (messageSize == sizeof(flightStatusMessage)) {
        flightStatusMessage flightStatus = parseFlightStatusMessage(message);
        if (flightStatus.isInEVA()) {
          gameStatus = EVA;
          return;
        }
        if (flightStatus.isInFligth()) {
          gameStatus = Flight;
          return;
        }
      }
      break;
    case SAS_MODE_INFO_MESSAGE:
      if (messageSize == sizeof(SASInfoMessage)) {
        SASInfoMessage sasMessage = parseSASInfoMessage(message);
        switch (sasMessage.currentSASMode) {
          case AP_STABILITYASSIST:
            currentSASMode = FreeLed;
            break;
          case AP_MANEUVER:
            currentSASMode = ManeuverLed;
            break;
          case AP_PROGRADE:
            currentSASMode = ProgradeLed;
            break;
          case AP_RETROGRADE:
            currentSASMode = RetrogradeLed;
            break;
          case AP_NORMAL:
            currentSASMode = NormalLed;
            break;
          case AP_ANTINORMAL:
            currentSASMode = AntiNormalLed;
            break;
          case AP_RADIALOUT:
            currentSASMode = RadialOutLed;
            break;
          case AP_RADIALIN:
            currentSASMode = RadialInLed;
            break;
          case AP_TARGET:
            currentSASMode = TargetLed;
            break;
          case AP_ANTITARGET:
            currentSASMode = AntiTargetLed;
            break;
          default:
            currentSASMode = OffLed;
            break;
        }
      }
    default:
      break;
  }
}
