using IntelligentHabitacion.App.Services;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.IntelligentHabitacionDevice
{
    public static class IntelligentHabitacionDevice
    {
        public static double Width()
        {
            return Resolver.Resolve<UserPreferences>().Width;
        }
    }
}
