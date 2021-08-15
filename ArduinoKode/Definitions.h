#ifndef _definitions_h
#define _definitions_h

#define MinOutput -32768
#define MaxOutput 32767
#define PrecisionMinOutput -10000
#define PrecisionMaxOutput 10000
#define MinThrottle 0
#define MaxThrottle 350
#define HalfInput 512
#define Deadzone 100

#define DebounceDelay 80
#define ButtonsToDebounce 45

#define LeftJoyStickX A3
#define LeftJoyStickY A2
#define ExtraLeftJoyStickX A1
#define RightJoyStickX A4
#define RightJoyStickY A5
#define ExtraRightJoyStickY A6
#define AnalogThrottle A0

#define PrecisionLeftButton 11
#define PrecisionRightButton 12

#define StageButton 49
#define AbortButton 50

#define SASSwitch 27
#define RCSSwitch 28
#define LandingGearSwitch 29
#define LightsSwitch 30
#define BrakesSwitch 31

#define KeyQ 37
#define KeyW 38
#define KeyE 39
#define KeyA 40
#define KeyS 41
#define KeyD 42
#define KeyC 43
#define KeyF 44
#define KeyB 45
#define KeyR 46
#define KeyP 47
#define KeyI 48

#define Action1Button 32
#define Action2Button 33
#define Action3Button 34
#define Action4Button 35
#define Action5Button 36

#define SASFreeButton 13
#define SASManeuverButton 18
#define SASProgradeButton 51
#define SASRetrogradeButton 19
#define SASRadialInButton 16
#define SASRadialOutButton 21
#define SASNormalButton 52
#define SASAntiNormalButton 20
#define SASTargetButton 17
#define SASAntiTargetButton 26

#define DecreaseTimeWarpButton 2
#define IncreaseTimeWarpButton 7
#define PreviousVesselButton 3
#define NextVesselButton 8
#define CameraCycleButton 4
#define OrbitalViewButton 9
#define QuickSaveButton 5
#define QuickLoadButton 10
#define PauseButton 6

#define SASLedS0 22
#define SASLedS1 23
#define SASLedS2 24
#define SASLedS3 25

#define OffLed 0x00
#define FreeLed 0x01
#define ManeuverLed 0x02
#define ProgradeLed 0x03
#define RetrogradeLed 0x04
#define NormalLed 0x05
#define AntiNormalLed 0x06
#define RadialInLed 0x07
#define RadialOutLed 0x08
#define TargetLed 0x09
#define AntiTargetLed 0x0A

#define InputA 0x41
#define InputB 0x42
#define InputC 0x43
#define InputD 0x44
#define InputE 0x45
#define InputF 0x46
#define InputI 0x49
#define InputV 0x56
#define InputM 0x4D
#define InputP 0x50
#define InputQ 0x51
#define InputR 0x52
#define InputS 0x53
#define InputW 0x57
#define Input1 0x31
#define Input2 0x32
#define Input3 0x33
#define Input4 0x34
#define Input5 0x35
#define InputSpace 0x20
#define InputShift 0xA0
#define InputCtrl 0xA2
#define InputESC 0x1B
#define InputPlus 0xBB
#define InputMinus 0xBD
#define InputF5 0x74
#define InputF9 0x78
#define InputF12 0x7B
#define InputComma 0xBC
#define InputDot 0xBE

#endif
