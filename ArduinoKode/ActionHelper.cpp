#include "ActionHelper.h"
#include "PinHelper.h"
#include "KerbalActions.h"

void ReadActionButtons(){
  ReadButton(21, Press1);
  ReadButton(22, Press2);
  ReadButton(23, Press3);
  ReadButton(24, Press4);
  //ReadButton(25, Press5);
}

void ReadKeys(){
  ReadButton(9, PressQ);
  ReadButton(10, PressW);
  ReadButton(11, PressE);
  ReadButton(12, PressA);
  ReadButton(13, PressS);
  ReadButton(14, PressD);
  ReadButton(15, PressC);
  ReadButton(16, PressF);
  ReadButton(17, PressB);
  ReadButton(18, PressR);
  ReadButton(19, PressP);
  ReadButton(20, PressI);
}

void ReadSASButtons(){
  ReadButton(26, SetSASFree);
  ReadButton(27, SetSASManeuver);
  ReadButton(28, SetSASPrograde);
  ReadButton(29, SetSASRetrograde);
  ReadButton(30, SetSASRadialIn);
  ReadButton(31, SetSASRadialOut);
  ReadButton(32, SetSASNormal);
  ReadButton(33, SetSASAntiNormal);
  ReadButton(34, SetSASTarget);
  ReadButton(35, SetSASAntiTarget);
}

void ReadGameButtons(){
  ReadButton(36, DecreaseTimeWarp);
  ReadButton(37, IncreaseTimeWarp);
  ReadButton(38, PreviousVessel);
  ReadButton(39, NextVessel);
  ReadButton(40, CameraCycle);
  ReadButton(41, OrbitalView);
  ReadButton(42, QuickSave);
  ReadButton(43, QuickLoad);
  ReadButton(44, Pause);
}

void ReadSwitches(){
  ReadSwitch(4, ToggleLandingGear);
  ReadSwitch(5, ToggleBrakes);
  ReadSwitch(6, ToggleLights);
  ReadSwitch(7, ToggleSAS);
  ReadSwitch(8, ToggleRCS);
}

void ReadAbortStage(){
  ReadButton(2, Abort);
  ReadButton(3, Stage);
}

void ReadPrecisionButtons(){
  ReadButton(0, SetLeftPrecision);
  ReadButton(1, SetRightPrecision);
}
