using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;
using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Clients
{
    public class ArduinoClient : IHardwareClient
    {
        private const float HALF_MAXIMUM_INPUT = 511.5f;

        private readonly ArduinoDriver.ArduinoDriver ArduinoDriver;
        private readonly PinConfiguration pinConfiguration;
        private readonly float deadZoneThreshold;

        public ArduinoClient(PinConfiguration pinConfiguration, AppSettings appSettings, ArduinoModel model)
        {
            ArduinoDriver = new ArduinoDriver.ArduinoDriver(model, true);
            this.pinConfiguration = pinConfiguration;
            deadZoneThreshold = appSettings.JoystickDeadZone;
        }

        public JoystickAxis ReadLeftJoystick()
        {
            return new JoystickAxis
            {
                XValue = ReadAnalogPin(pinConfiguration.LeftJoyStickX),
                YValue = ReadAnalogPin(pinConfiguration.LeftJoyStickY, true)
            };
        }

        public JoystickAxis ReadRightJoystick()
        {
            throw new NotImplementedException();
        }

        private float ReadAnalogPin(byte pin, bool inverted = false)
        {
            var analogResponse = ArduinoDriver.Send(new AnalogReadRequest(pin));
            return AnalogConversor(analogResponse.PinValue, inverted);
        }

        private float AnalogConversor(int analogValue, bool inverted)
        {
            var deadZone = deadZoneThreshold;
            var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

            if (Math.Abs(convertedValue) < deadZone) return 0;
            return inverted ? - convertedValue : convertedValue;
        }
    }
}
