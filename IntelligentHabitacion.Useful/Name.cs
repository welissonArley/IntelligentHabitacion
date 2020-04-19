namespace IntelligentHabitacion.Useful
{
    public static class Name
    {
        public static string ShortNameConverter(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            else
            {
                var words = value.Split(' ');
                return (words.Length == 1 ? $"{words[0][0]}" : $"{words[0][0]}{words[1][0]}").ToUpper();
            }
        }
    }
}
