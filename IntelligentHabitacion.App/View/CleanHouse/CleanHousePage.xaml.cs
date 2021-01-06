﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.CleanHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CleanHousePage : ContentPage
    {
        public CleanHousePage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            ((ListView)sender).BackgroundColor = Color.Transparent;
        }
    }
}