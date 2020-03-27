using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View
{
    [DesignTimeVisible(false)]
    public partial class AboutThisAppPage : ContentPage
    {
        public AboutThisAppPage()
        {
            InitializeComponent();
        }

        private void Flaticon_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.flaticon.com"));
        }
        private void Freepick_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://br.freepik.com"));
        }
        private void Undraw_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://undraw.co/illustrations"));
        }
        private void Drawkit_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://www.drawkit.io"));
        }
    }
}
