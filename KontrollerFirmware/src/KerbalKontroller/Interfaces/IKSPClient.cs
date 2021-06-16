using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IKSPClient
    {
        object GetActiveVessel();
        void UpdateActiveVessel();
        bool IsInFlight();
        bool IsInEditor();
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
        SASModes GetSASMode();
        void SetRCSMode(DigitalState digitalState);
        void Abort();
        void Stage();
        void ActivateAction(uint actionGroup);
        void SetSASMode(SASModes sasMode);
    }
}
