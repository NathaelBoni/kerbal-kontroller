using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Interfaces
{
    public interface IKSPClient
    {
        Action GetActiveVesselControl();
        bool IsInFlight();
        bool IsGamePaused();
        void SetPaused();
        void QuickSave();
        void QuickLoad();
        void SetVesselRotation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra);
        void SetVesselTranslation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra);
        void SetPlaneRotation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra);
        void SetPlaneTranslation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra);
        void SetRoverMovement(JoystickAxis joystickAxis);
        void SetThrottle(JoystickAxis joystickAxis);
        void SetLandingGear(DigitalState digitalState);
        void SetBrakes(DigitalState digitalState);
        void SetLights(DigitalState digitalState);
        void SetSASActive(DigitalState digitalState);
        void SetRCSMode(DigitalState digitalState);
        void Abort();
        void Stage();
        void ActivateAction(uint actionGroup);
        SASModes GetSASMode();
        void SetSASMode(SASModes sasMode);
        void SetSASModeFree();
        void SetSASModeManeuver();
        void SetSASModePrograde();
        void SetSASModeRetrograde();
        void SetSASModeNormal();
        void SetSASModeAntiNormal();
        void SetSASModeRadialIn();
        void SetSASModeRadialOut();
        void SetSASModeTarget();
        void SetSASModeAntiTarget();
    }
}
