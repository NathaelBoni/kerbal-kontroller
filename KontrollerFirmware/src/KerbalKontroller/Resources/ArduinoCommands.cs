namespace KerbalKontroller.Resources
{
    public static class ArduinoCommands
    {
        public static byte Nop => 0x00;
        public static byte Input => 0x01;
        public static byte Output => 0x02;
        public static byte Configured => 0x10;
        public static byte SetSASLed => 0x20;
    }

    public static class ArduinoArguments
    {
        public static byte NoArg => 0x00;
        public static byte FreeLed => 0x01;
        public static byte ManeuverLed => 0x02;
        public static byte ProgradeLed => 0x03;
        public static byte RetrogradeLed => 0x04;
        public static byte NormalLed => 0x05;
        public static byte AntiNormalLed => 0x06;
        public static byte RadialOutLed => 0x07;
        public static byte RadialInLed => 0x08;
        public static byte TargetLed => 0x09;
        public static byte AntiTargetLed => 0x0A;
    }
}
