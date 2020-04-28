using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFoodsComponent : ContentView
    {
        public FoodModel Food
        {
            get => (FoodModel)GetValue(FoodProperty);
            set => SetValue(FoodProperty, value);
        }

        public static readonly BindableProperty FoodProperty = BindableProperty.Create(
                                                        propertyName: "Food",
                                                        returnType: typeof(FoodModel),
                                                        declaringType: typeof(MyFoodsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FoodChanged);

        public static readonly BindableProperty TappedItemCommandProperty = BindableProperty.Create(propertyName: "TappedItem",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedItemCommand
        {
            get => (ICommand)GetValue(TappedItemCommandProperty);
            set => SetValue(TappedItemCommandProperty, value);
        }

        private static void FoodChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue != null)
            {
                var foodModel = (FoodModel)newValue;
                var component = ((MyFoodsComponent)bindable);
                component.Product.Text = foodModel.Name;

                component.Description.Text = $"{foodModel.Amount} {GetEnumDescription.Description(foodModel.Type)}{(foodModel.DueDate is null ? "" : $" | {ResourceText.TITLE_DUEDATE_TWOPOINTS} {foodModel.DueDate.Value:dd MMMM yyyy}")}";
            }
        }

        public MyFoodsComponent()
        {
            InitializeComponent();
        }

        private void Item_Tapped(object sender, System.EventArgs e)
        {
            TappedItemCommand?.Execute(Food);
        }
    }
}