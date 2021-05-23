using Com.OneSignal.Abstractions;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.View.Dashboard.PartOfHome;
using IntelligentHabitacion.App.View.Login;
using IntelligentHabitacion.App.ViewModel.Login;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
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
            var userPreferences = Resolver.Resolve<UserPreferences>();
            var key = notification.payload.additionalData.Keys.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(key))
                return;

            switch (key)
            {
                case EnumNotifications.OrderReceived:
                    {
                        userPreferences.UserHasOrder(true);
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.NewAdmin:
                    {
                        userPreferences.UserIsAdministrator(true);
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.RemovedFromHome:
                case EnumNotifications.HomeDeleted:
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            userPreferences.UserIsPartOfOneHome(false);
                            var navigation = Resolver.Resolve<INavigation>();
                            var page = navigation.NavigationStack.FirstOrDefault();
                            if (page is UserIsPartOfHomeFlyoutPage)
                            {
                                try { await navigation.PopAllPopupAsync(); } catch { /* If one exception is throwed its beacause dont have any popup */ }
                                await navigation.PopToRootAsync();
                                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<UserWithoutPartOfHomeViewModel, UserWithoutPartOfHomePage>());
                            }
                        });
                    }
                    break;
            }
        }

        private static void RefreshHeader()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                var page = navigation.NavigationStack.FirstOrDefault();
                if (page is UserIsPartOfHomeFlyoutPage refreshPage)
                {
                    var pageDetail = ((FlyoutPage)page).Detail;
                    ((UserIsPartOfHomeFlyoutPageDetail)pageDetail).RefreshHeader();
                }
            });
        }
    }
}
