using System;
using Xamarin.Essentials;

namespace IntelligentHabitacion.App.Useful
{
    public static class Phonecall
    {
        public static void Make(string number)
        {
            Launcher.OpenAsync(new Uri($"tel:{number.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")}"));
        }
    }
}
