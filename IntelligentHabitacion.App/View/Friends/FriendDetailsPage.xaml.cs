using IntelligentHabitacion.App.SQLite.Interface;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Friends
{
    [DesignTimeVisible(false)]
    public partial class FriendDetailsPage : ContentPage
    {
        public FriendDetailsPage()
        {
            InitializeComponent();
            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();
            if (!database.Get().IsAdministrator)
                ToolbarItems.Clear();
        }
    }
}
