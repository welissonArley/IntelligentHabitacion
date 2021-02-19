using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeInformationsRoomTemplate : ContentView
    {
        public IList<RoomModel> Rooms
        {
            get => (IList<RoomModel>)GetValue(RoomsProperty);
            set => SetValue(RoomsProperty, value);
        }

        public static readonly BindableProperty RoomsProperty = BindableProperty.Create(
                                                        propertyName: "Rooms",
                                                        returnType: typeof(IList<RoomModel>),
                                                        declaringType: typeof(HomeInformationsRoomTemplate),
                                                        defaultValue: new List<RoomModel>(),
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: RoomsChanged);

        private static void RoomsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var rooms = (IEnumerable<RoomModel>)newValue;
                var component = ((HomeInformationsRoomTemplate)bindable);

                component.Content.Children.Clear();

                var isAdministrator = XLabs.Ioc.Resolver.Resolve<UserPreferences>().IsAdministrator;

                foreach (var room in rooms)
                    component.Content.Children.Add(CreateContentRoom(room.Room, isAdministrator));

                if (isAdministrator)
                {
                    component.Content.Children.Add(new Xamarin.Forms.Button
                    {
                        FontSize = 16,
                        BackgroundColor = Color.White,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        TextColor = (Color)Application.Current.Resources["YellowDefault"],
                        Text = ResourceText.TITLE_ADD_ROOM
                    });
                }
            }
        }

        private static StackLayout CreateContentRoom(string room, bool isAdministrator)
        {
            return new StackLayout
            {
                Margin = new Thickness(0,20,0,0),
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Text = room,
                                FontSize = 14
                            },
                            new Image
                            {
                                HeightRequest = 15,
                                WidthRequest = 15,
                                HorizontalOptions = LayoutOptions.EndAndExpand,
                                Source = ImageSource.FromFile("IconDelete"),
                                IsVisible = isAdministrator
                            }
                        }
                    },
                    new BoxView
                    {
                        HeightRequest = 1,
                        Opacity = 0.2,
                        Color = (Color)Application.Current.Resources["GrayDefault"]
                    }
                }
            };
        }

        public HomeInformationsRoomTemplate()
        {
            InitializeComponent();
        }
    }
}