using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.Useful;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendsComponent : ContentView
    {
        public FriendModel Friend
        {
            get => (FriendModel)GetValue(FriendProperty);
            set => SetValue(FriendProperty, value);
        }

        public static readonly BindableProperty FriendProperty = BindableProperty.Create(
                                                        propertyName: "Friend",
                                                        returnType: typeof(FriendModel),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendChanged);

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty TappedItemCommandProperty = BindableProperty.Create(propertyName: "TappedItem",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedMakePhonecallCommand
        {
            get => (ICommand)GetValue(TappedMakePhonecallCommandProperty);
            set => SetValue(TappedMakePhonecallCommandProperty, value);
        }

        public ICommand TappedItemCommand
        {
            get => (ICommand)GetValue(TappedItemCommandProperty);
            set => SetValue(TappedItemCommandProperty, value);
        }

        private static void FriendChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue != null)
            {
                var friendModel = (FriendModel)newValue;
                var component = ((MyFriendsComponent)bindable);
                component.LabelFriendsName.Text = friendModel.Name;
                component.LabelShortName.Text = Name.ShortNameConverter(friendModel.Name);
                component.BackgroundShortName.BackgroundColor = Xamarin.Forms.Color.FromHex(friendModel.ProfileColor);
                component.BackgroundCall.BackgroundColor = Xamarin.Forms.Color.FromHex(friendModel.ProfileColor);
                component.LabelJoinedOn.Text = string.Format(ResourceText.TITLE_JOINED_ON, friendModel.JoinedOn.ToString(ResourceText.FORMAT_DATE));
            }
        }

        public MyFriendsComponent()
        {
            InitializeComponent();
        }

        private void MakePhoneCall_Tapped(object sender, System.EventArgs e)
        {
            TappedMakePhonecallCommand?.Execute(Friend);
        }

        private void Item_Tapped(object sender, System.EventArgs e)
        {
            TappedItemCommand?.Execute(Friend);
        }
    }
}