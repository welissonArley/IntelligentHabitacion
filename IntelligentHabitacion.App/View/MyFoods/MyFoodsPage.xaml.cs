using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.MyFoods
{
    [DesignTimeVisible(false)]
    public partial class MyFoodsPage : ContentPage
    {
        public MyFoodsPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            ((ListView)sender).BackgroundColor = Color.Transparent;
        }
    }
}