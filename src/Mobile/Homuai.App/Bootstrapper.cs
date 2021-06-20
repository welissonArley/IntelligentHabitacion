using Homuai.App.Services;
using XLabs.Ioc;

namespace Homuai.App
{
    public static class Bootstrapper
    {
        public static IDependencyContainer AddDependeces(this IDependencyContainer container)
        {
            return container.Register(new UserPreferences());
        }
    }
}
