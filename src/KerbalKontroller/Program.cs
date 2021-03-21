using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;
using KRPC.Client.Services.SpaceCenter;
using System;

namespace KerbalKontroller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = new KRPC.Client.Connection("ksp");
            var vessel = connection.SpaceCenter().ActiveVessel;

            var driver = new ArduinoDriver.ArduinoDriver(ArduinoModel.Leonardo, true);

            var valX = driver.Send(new AnalogReadRequest(0)).PinValue;

            vessel.Control.Pitch = AnalogConversor(valX);
        }

        private static float AnalogConversor(int analogValue)
        {
            var deadZone = 0.018f;
            var convertedValue = (analogValue / 511.5f) - 1;

            if (Math.Abs(convertedValue) < deadZone) return 0;
            return convertedValue;
        }
    }
}
