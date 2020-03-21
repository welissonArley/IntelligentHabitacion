using System;
using System.Linq;

namespace IntelligentHabitacion.Useful
{
    public class CodeGenerator
    {
        public string Random()
        {
            Random random = new Random();
            string chars = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6Q7R8S9T0U1V2W3X4Y5Z6";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
