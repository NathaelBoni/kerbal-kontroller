using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;
using System;

namespace KerbalKontroller
{
    public class ArduinoFacade
    {
        private const float DEAD_ZONE_THRESHOLD = 0.018f;
        private const float HALF_MAXIMUM_INPUT = 511.5f;

        private readonly ArduinoDriver.ArduinoDriver ArduinoDriver;

        public ArduinoFacade()
        {
            ArduinoDriver = new ArduinoDriver.ArduinoDriver(ArduinoModel.Leonardo, true);
        }

        public JoystickAxis ReadLeftJoystick()
        {
            return new JoystickAxis
            {
                XValue = ReadAnalogPin(0),
                YValue = ReadAnalogPin(1, true)
            };
        }

        private float ReadAnalogPin(byte pin, bool inverted = false)
        {
            var analogResponse = ArduinoDriver.Send(new AnalogReadRequest(pin));
            return AnalogConversor(analogResponse.PinValue, inverted);
        }

        private float AnalogConversor(int analogValue, bool inverted)
        {
            var deadZone = DEAD_ZONE_THRESHOLD;
            var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

            if (Math.Abs(convertedValue) < deadZone) return 0;
            return inverted ? - convertedValue : convertedValue;
        }
    }

    public class JoystickAxis
    {
        public float XValue { get; set; }
        public float YValue { get; set; }
    }
}
