using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.Template.TextWithLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelComponent : ContentView
    {
        public string LabelTitle { get; set; }
        public string PlaceHolderText { set { Input.Placeholder = value; } }
        public Keyboard Keyboard { set { Input.Keyboard = value; } }
        public bool IsPassword { set { Input.IsPassword = value; } }

        public InputTextWithLabelComponent()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    Label.Text = " ";
                else
                    Label.Text = LabelTitle;
            };
        }

        public string TypedText()
        {
            return Input.Text?.Trim();
        }
    }
}