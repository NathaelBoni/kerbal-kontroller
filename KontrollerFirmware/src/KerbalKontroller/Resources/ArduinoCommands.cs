namespace KerbalKontroller.Resources
{
    public static class ArduinoCommands
    {
        public static byte Nop => 0x00;
        public static byte Input => 0x01;
        public static byte Output => 0x02;
        public static byte OutputSASLeds => 0x03;
        public static byte Configured => 0x10;
        public static byte SetSASLed => 0x20;
    }
}
