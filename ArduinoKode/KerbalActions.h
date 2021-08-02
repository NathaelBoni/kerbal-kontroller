#ifndef _kerbal_actions_h
#define _kerbal_actions_h

#include <Arduino.h>
#include "KerbalSimpit.h"

enum WalkingDirectionX {
  XStop,
  Left,
  Right
};

enum WalkingDirectionY {
  YStop,
  Backward,
  Forward
};

enum WalkingDirectionZ {
  ZStop,
  Down,
  Up
};

enum WalkingDirectionRoll {
  RollStop,
  AntiClockWise,
  ClockWise
};

enum GameStatus {
  Flight,
  EVA
};

enum FlightType {
  SpaceShip,
  Plane,
  Rover
};

extern KerbalSimpit simpit;

extern timewarpMessage timewarp;

extern bool leftPrecisionEnabled, rightPrecisionEnabled;
extern int leftMaxValue, leftMinValue;
extern int rightMaxValue, rightMinValue;

extern GameStatus gameStatus;
extern FlightType flightType;
extern byte currentSASMode;
extern WalkingDirectionX currentXDirection, lastXDirection;
extern WalkingDirectionY currentYDirection, lastYDirection;
extern WalkingDirectionZ currentZDirection, lastZDirection;

void InitializeActions();

void ProcessRotationSpaceShip();
void ProcessRotationPlane();
void ProcessTranslation();
void ProcessKerbalWalking();
void ProcessRoverWheels();
void ProcessThrottle();

void PressQ();
void PressW();
void PressE();
void PressA();
void PressS();
void PressD();
void PressC();
void PressF();
void PressB();
void PressR();
void PressP();
void PressI();
void PressShift();
void PressCtrl();
void Press1();
void Press2();
void Press3();
void Press4();
void Press5();

void ToggleLandingGear();
void ToggleBrakes();
void ToggleLights();
void ToggleSAS();
void ToggleRCS();

void Abort();
void Stage();

void SetSASFree();
void SetSASManeuver();
void SetSASPrograde();
void SetSASRetrograde();
void SetSASRadialIn();
void SetSASRadialOut();
void SetSASNormal();
void SetSASAntiNormal();
void SetSASTarget();
void SetSASAntiTarget();

void DecreaseTimeWarp();
void IncreaseTimeWarp();
void PreviousVessel();
void NextVessel();
void CameraCycle();
void OrbitalView();
void QuickSave();
void QuickLoad();
void Pause();

void SetLeftPrecision();
void SetRightPrecision();

void SimpitUpdate();
void CallbackHandler(byte messageType, byte message[], byte messageSize);

#endif
