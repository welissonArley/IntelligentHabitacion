using System;
using System.Linq;

namespace IntelligentHabitacion.Useful
{
    public class CodeGenerator
    {
        public string Random6Chars()
        {
            Random random = new Random();
            string chars = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6Q7R8S9T0U1V2W3X4Y5Z6";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string Random36Chars()
        {
            Random random = new Random();
            string chars = "A1B2*C3D(4E5F!6G7)H8I;9J0K+1L2M3N-4O5^P6Q7[R8S$9T0U|1V2@W3X4Y$5Z6}";
            return new string(Enumerable.Repeat(chars, 36).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
