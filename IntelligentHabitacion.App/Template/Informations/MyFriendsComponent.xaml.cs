using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Useful;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendsComponent : ContentView
    {
        private static FriendModel _friend;
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

        private static void FriendChanged(BindableObject bindable, object oldValue, object newValue)
        {
            _friend = (FriendModel)newValue;
            var component = ((MyFriendsComponent)bindable);
            component.LabelFriendsName.Text = _friend.Name;
            component.LabelShortName.Text = Name.ShortNameConverter(_friend.Name);
            component.BackgroundShortName.BackgroundColor = Xamarin.Forms.Color.FromHex(_friend.ProfileColor);
            component.BackgroundCall.BackgroundColor = Xamarin.Forms.Color.FromHex(_friend.ProfileColor);
            component.LabelJoinedOn.Text = string.Format(ResourceText.TITLE_JOINED_ON, _friend.JoinedOn.ToString(ResourceText.FORMAT_DATE));
        }

        public MyFriendsComponent()
        {
            InitializeComponent();
        }
    }
}