using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View
{
    [DesignTimeVisible(false)]
    public partial class AboutThisAppPage : ContentPage
    {
        private readonly string _urlFlatIcone;
        private readonly string _urlFreepick;
        private readonly string _urlUndraw;
        private readonly string _urlDrawkit;

        public AboutThisAppPage()
        {
            InitializeComponent();

            _urlFlatIcone = "https://www.flaticon.com";
            _urlFreepick = "https://br.freepik.com";
            _urlUndraw = "https://undraw.co/illustrations";
            _urlDrawkit = "https://www.drawkit.io";
        }

        private void Flaticon_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(_urlFlatIcone));
        }
        private void Freepick_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(_urlFreepick));
        }
        private void Undraw_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(_urlUndraw));
        }
        private void Drawkit_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(_urlDrawkit));
        }
    }
}
