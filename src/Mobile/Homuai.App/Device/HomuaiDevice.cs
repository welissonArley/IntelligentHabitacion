﻿using Homuai.App.Services;
using XLabs.Ioc;

namespace Homuai.App.Device
{
    public static class HomuaiDevice
    {
        public static double Width()
        {
            return Resolver.Resolve<UserPreferences>().Width;
        }
    }
}
