﻿using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SetOfRules.Rule;
using IntelligentHabitacion.App.SQLite;
using IntelligentHabitacion.App.SQLite.Interface;
using XLabs.Ioc;

namespace IntelligentHabitacion.App
{
    public static class Bootstrapper
    {
        public static void Register(IDependencyContainer container)
        {
            container.Register<ISqliteDatabase, SqliteDatabase>();
            container.Register<IHomeRule, HomeRule>();
            container.Register<ILoginRule, LoginRule>();
            container.Register<IMyFoodsRule, MyFoodsRule>();
            container.Register<IUserRule, UserRule>();
        }
    }
}