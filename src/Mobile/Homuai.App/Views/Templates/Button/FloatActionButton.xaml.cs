﻿using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatActionButton : ContentView
    {
        public static readonly BindableProperty TappedButtonCommandProperty = BindableProperty.Create(propertyName: "TappedButton",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(ConfirmDangerActionButton),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedButtonCommand
        {
            get => (ICommand)GetValue(TappedButtonCommandProperty);
            set => SetValue(TappedButtonCommandProperty, value);
        }

        public void Button_OnTapped(object sender, System.EventArgs e)
        {
            TappedButtonCommand?.Execute(null);
        }

        public FloatActionButton()
        {
            InitializeComponent();
        }
    }
}