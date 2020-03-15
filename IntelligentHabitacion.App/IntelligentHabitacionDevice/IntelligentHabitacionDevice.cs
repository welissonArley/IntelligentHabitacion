using IntelligentHabitacion.App.SQLite.Interface;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.IntelligentHabitacionDevice
{
    public static class IntelligentHabitacionDevice
    {
        public static double Width()
        {
            return Resolver.Resolve<ISqliteDatabase>().Get().Width;
        }
    }
}
