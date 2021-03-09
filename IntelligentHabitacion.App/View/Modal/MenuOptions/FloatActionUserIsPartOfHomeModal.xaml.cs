﻿using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionUserIsPartOfHomeModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public FloatActionUserIsPartOfHomeModal()
        {
            InitializeComponent();
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }
    }
}
