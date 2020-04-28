using System;
using System.ComponentModel;

namespace IntelligentHabitacion.App.Model
{
    public enum Type
    {
        [Description("TITLE_UNITY")]
        Unity = 0,
        [Description("TITLE_BOX")]
        Box = 1,
        [Description("TITLE_PACKAGE")]
        Package = 2,
        [Description("TITLE_KILOGRAM")]
        Kilogram = 3
    }

    public class FoodModel
    {
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public double Amount { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
    }
}
