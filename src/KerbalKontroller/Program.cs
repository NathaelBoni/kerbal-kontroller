namespace KerbalKontroller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var krpc = new KRPCClient();
            var driver = new ArduinoFacade();

            var vessel = krpc.GetActiveVessel();

            while (true)
            {
                var leftJoystick = driver.ReadLeftJoystick();
                vessel.Control.Yaw = leftJoystick.XValue;
                vessel.Control.Pitch = leftJoystick.YValue;
            }
        }
    }
}
