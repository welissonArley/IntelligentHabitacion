using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    public enum TypeDueDate
    {
        ExpiredProduct = 0,
        NextDueDate = 1,
        OkProduct = 2
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFoodsComponent : ContentView
    {
        public FoodModel Food
        {
            get => (FoodModel)GetValue(FoodProperty);
            set => SetValue(FoodProperty, value);
        }
        public ICommand TappedItemCommand
        {
            get => (ICommand)GetValue(TappedItemCommandProperty);
            set => SetValue(TappedItemCommandProperty, value);
        }
        public ICommand TappedChangeAmountCommand
        {
            get => (ICommand)GetValue(TappedChangeAmountCommandProperty);
            set => SetValue(TappedChangeAmountCommandProperty, value);
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

        public static readonly BindableProperty TappedChangeAmountCommandProperty = BindableProperty.Create(propertyName: "TappedChangeAmount",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void FoodChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue != null)
            {
                var foodModel = (FoodModel)newValue;
                var component = ((MyFoodsComponent)bindable);
                component.Product.Text = foodModel.Name;
                component.DueDateController.Text = $"{(int)GetTypeDueDate(foodModel)}";
                component.Description.Text = DescriptionToShow(foodModel);
            }
        }

        public MyFoodsComponent()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            Product.Text = Food.Name;
            DueDateController.Text = $"{(int)GetTypeDueDate(Food)}";
            Description.Text = DescriptionToShow(Food);
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            TappedItemCommand?.Execute(this);
        }

        private void Button_AddOne(object sender, EventArgs e)
        {
            Food.Amount++;
            Description.Text = DescriptionToShow(Food);
            TappedChangeAmountCommand?.Execute(Food);
        }

        private void Button_SubtractOne(object sender, EventArgs e)
        {
            Food.Amount--;
            Description.Text = DescriptionToShow(Food);
            TappedChangeAmountCommand?.Execute(Food);
        }

        private static string DescriptionToShow(FoodModel foodModel)
        {
            return $"{foodModel.Amount} {GetEnumDescription.Description(foodModel.Type)}{(foodModel.DueDate is null ? "" : $" | {ResourceText.TITLE_DUEDATE_TWOPOINTS} {foodModel.DueDate.Value:dd MMM yyyy}")}";
        }
        private static TypeDueDate GetTypeDueDate(FoodModel foodModel)
        {
            if (foodModel.DueDate is null)
                return TypeDueDate.OkProduct;

            var totalDays = (foodModel.DueDate.Value.Date - DateTime.Today).TotalDays;

            if (totalDays <= 0)
                return TypeDueDate.ExpiredProduct;

            if (totalDays <= 3)
                return TypeDueDate.NextDueDate;

            return TypeDueDate.OkProduct;
        }
    }
}