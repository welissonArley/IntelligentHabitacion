using XLabs.Ioc;

namespace IntelligentHabitacion.Communication
{
    public static class Bootstrapper
    {
        public static void Register(IDependencyContainer container)
        {
            container.RegisterSingle<IIntelligentHabitacionHttpClient, IntelligentHabitacionHttpClient>();
        }
    }
}
