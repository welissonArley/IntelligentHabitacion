using Android.Content;
using IntelligentHabitacion.CustomControl;
using IntelligentHabitacion.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelJustifyText), typeof(LabelJustifyTextRenderer))]
namespace IntelligentHabitacion.Droid.CustomControl
{
    public class LabelJustifyTextRenderer : LabelRenderer
    {
        public LabelJustifyTextRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.JustificationMode = Android.Text.JustificationMode.InterWord;
            }
        }
    }
}