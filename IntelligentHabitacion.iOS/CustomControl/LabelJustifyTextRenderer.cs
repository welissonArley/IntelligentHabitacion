using IntelligentHabitacion.CustomControl;
using IntelligentHabitacion.iOS.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabelJustifyText), typeof(LabelJustifyTextRenderer))]
namespace IntelligentHabitacion.iOS.CustomControl
{
    public class LabelJustifyTextRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextAlignment = UIKit.UITextAlignment.Justified;
            }
        }
    }
}