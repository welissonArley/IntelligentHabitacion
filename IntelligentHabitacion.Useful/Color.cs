using System;

namespace IntelligentHabitacion.Useful
{
    public static class Color
    {
        private enum ColorToUse
        {
            Color1 = 1,
            Color2 = 2,
            Color3 = 3,
            Color4 = 4,
            Color5 = 5,
            Color6 = 6,
            Color7 = 7,
            Color8 = 8,
            Color9 = 9,
            Color10 = 10,
            Color11 = 11,
            Color12 = 12,
            Color13 = 13,
            Color14 = 14,
            Color15 = 15,
            Color16 = 16,
            Color17 = 17,
            Color18 = 18,
            Color19 = 19,
            Color20 = 20,
        };

        private static string GetColor(ColorToUse color)
        {
            switch (color)
            {
                case ColorToUse.Color1:
                    return "#00BD9D";
                case ColorToUse.Color2:
                    return "#1ECE6C";
                case ColorToUse.Color3:
                    return "#2C97DD";
                case ColorToUse.Color4:
                    return "#9C56B9";
                case ColorToUse.Color5:
                    return "#334960";
                case ColorToUse.Color6:
                    return "#F69E00";
                case ColorToUse.Color7:
                    return "#E87E04";
                case ColorToUse.Color8:
                    return "#EA4B35";
                case ColorToUse.Color9:
                    return "#7E8C8D";
                case ColorToUse.Color10:
                    return "#65BCBF";
                case ColorToUse.Color11:
                    return "#657EBF";
                case ColorToUse.Color12:
                    return "#BF9B65";
                case ColorToUse.Color13:
                    return "#BF658B";
                case ColorToUse.Color14:
                    return "#CB5E5E";
                case ColorToUse.Color15:
                    return "#5ECB69";
                case ColorToUse.Color16:
                    return "#5E97CB";
                case ColorToUse.Color17:
                    return "#285059";
                case ColorToUse.Color18:
                    return "#592828";
                case ColorToUse.Color19:
                    return "#2F2859";
                case ColorToUse.Color20:
                    return "#DB2365";
                default:
                    return "#000000";
            }
        }

        public static string RandomColor()
        {
            var colors = Enum.GetValues(typeof(ColorToUse));

            var index = new Random().Next(colors.Length - 1);
            return GetColor((ColorToUse)colors.GetValue(index));
        }
    }
}
