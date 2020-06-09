using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.App.View;
using IntelligentHabitacion.App.ViewModel;
using IntelligentHabitacion.Useful;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.OneSignalConfig
{
    public static class OneSignalManager
    {
        public static string OneSignalKey { get { return "658a8e23-65fe-450f-9bf8-9ef1c3d1abdc"; } }
        public static string MyOneSignalId { private set; get; }

        public static void SetMyIdOneSignal(string myId)
        {
            MyOneSignalId = myId;
        }

        public static void Notification(OSNotification notification)
        {
            var database = Resolver.Resolve<ISqliteDatabase>();
            var key = notification.payload.additionalData.Keys.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(key))
                return;

            switch (key)
            {
                case EnumNotifications.OrderReceived:
                    {
                        database.ReceivedOrder();
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.NewAdmin:
                    {
                        database.IsAdministrator();
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.RemovedFromHome:
                    {
                        Task.Run(() => Device.BeginInvokeOnMainThread(async() =>
                        {
                            var navigation = Resolver.Resolve<INavigation>();
                            var page = navigation.NavigationStack.FirstOrDefault();
                            if (page is UserIsPartOfHomePage)
                            {
                                try { await navigation.PopAllPopupAsync(); } catch { /* If one exception is throwed its beacous dont have any popup */ }
                                await navigation.PopToRootAsync();
                                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
                            }
                        }));
                    }
                    break;
            }
        }

        private static void RefreshHeader()
        {
            Task.Run(() => Device.BeginInvokeOnMainThread(() =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                var page = navigation.NavigationStack.FirstOrDefault();
                if (page is UserIsPartOfHomePage refreshPage)
                    refreshPage.RefreshHeader();
            }));
        }
    }
}
