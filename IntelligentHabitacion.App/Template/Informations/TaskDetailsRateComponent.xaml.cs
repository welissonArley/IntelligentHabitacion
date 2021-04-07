using IntelligentHabitacion.App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailsRateComponent : ContentView
    {
        public DetailsTaskCleanedOnDayModel TaskDetails
        {
            get => (DetailsTaskCleanedOnDayModel)GetValue(TaskDetailsProperty);
            set => SetValue(TaskDetailsProperty, value);
        }

        public static readonly BindableProperty TaskDetailsProperty = BindableProperty.Create(
                                                        propertyName: "TaskDetails",
                                                        returnType: typeof(DetailsTaskCleanedOnDayModel),
                                                        declaringType: typeof(TaskDetailsRateComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskDetailsChanged);

        private static void TaskDetailsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue == null && newValue != null ? newValue : oldValue;
            if(newValue != null)
            {
                var model = (DetailsTaskCleanedOnDayModel)newValue;
                var component = ((TaskDetailsRateComponent)bindable);

                component.UserLabel.Text = model.User;
                if (model.CanRate)
                {
                    component.SeeDetailsButton.IsVisible = false;
                    component.ContentShowRateDetails.IsVisible = false;
                    component.ButtonToRate.IsVisible = true;
                }
                else
                {
                    component.SeeDetailsButton.IsVisible = true;
                    component.ContentShowRateDetails.IsVisible = true;
                    component.ButtonToRate.IsVisible = false;

                    if(model.AverageRate < 0)
                    {
                        component.LabelWithoutRate.IsVisible = true;
                        component.RatingStars.IsVisible = false;
                        component.SeeDetailsButton.IsVisible = false;
                    }
                    else
                    {
                        component.LabelWithoutRate.IsVisible = false;
                        component.RatingStars.IsVisible = true;
                        component.RatingStars.Rating = model.AverageRate;
                    }
                }
            }
        }

        public TaskDetailsRateComponent()
        {
            InitializeComponent();
        }
    }
}