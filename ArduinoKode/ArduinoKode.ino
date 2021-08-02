#include "KerbalSimpit.h"
#include "PinHelper.h"
#include "KerbalActions.h"
#include "ActionHelper.h"

void setup(){
  InitializePinsArray();
  SetPinMode();
  InitializeActions();
}

void loop(){
  if(gameStatus == EVA){
    ProcessKerbalWalking();
  }else{
    ReadActionButtons();
    if(flightType == SpaceShip){
      ProcessRotationSpaceShip();
      ProcessTranslation();
      ProcessThrottle();
    }else if (flightType == Plane){
      ProcessRotationPlane();
      ProcessTranslation();
      ProcessThrottle();
    }else if (flightType == Rover){
      ProcessRoverWheels();
      ProcessTranslation();
    }

    ReadActionButtons();
    ReadSASButtons();
    SetSASLed(currentSASMode);
    ReadSwitches();
    ReadAbortStage();
    ReadPrecisionButtons();
  }

  ReadKeys();
  ReadGameButtons();

  SimpitUpdate();
}
