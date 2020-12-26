using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaTemplate : ContentView
    {
        public string PlaceHolderText { set => Input.Placeholder = value; }

        public TextAreaTemplate()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    LabelCount.Text = "0/255";
                else
                {
                    if (Input.Text.Length > 255)
                        Input.Text = Input.Text.Substring(0, 255);

                    LabelCount.Text = $"{Input.Text.Length}/{255}";
                }
            };
        }
    }
}