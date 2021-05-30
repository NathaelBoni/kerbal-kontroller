using System.Collections.Generic;
using System.Linq;

namespace KerbalKontroller.Resources
{
    public class ControllerFunctions
    {
        private readonly int digitalPins;
        private Dictionary<byte, int> controllerValues;

        public ControllerFunctions(int digitalPins)
        {
            this.digitalPins = digitalPins;
        }

        public void DeserializeData(string data)
        {
            var keys = data.Split('|')[0].Split(',').Select(_ => byte.Parse(_));
            var values = data.Split('|')[1].Split(',').Select(_ => int.Parse(_));
            controllerValues = new Dictionary<byte, int>();

            var index = 0;
            foreach(var key in keys)
            {
                controllerValues.Add(key, values.ElementAt(index));
                index++;
            }
        }

        public int GetAnalogValue(byte pin)
        {
            byte.TryParse((pin + digitalPins).ToString(), out var idx);
            
            try
            {
                return controllerValues[idx];
            }
            catch
            {
                return 0;
            }
        }

        public bool GetDigitalValue(byte pin)
        {
            try
            {
                return controllerValues[pin] == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
