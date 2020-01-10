using CoreAnimation;
using CoreGraphics;
using IntelligentHabitacion.CustomControl;
using IntelligentHabitacion.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryGrayBlackCursor), typeof(EntryGrayBlackCursorRenderer))]
namespace IntelligentHabitacion.iOS.CustomControl
{
    public class EntryGrayBlackCursorRenderer : EntryRenderer
    {
        private CALayer _line;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
			base.OnElementChanged(e);
			_line = null;

			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;

			_line = new CALayer
			{
                BorderColor = ColorToEntryLine(),
                BackgroundColor = ColorToEntryLine(),
                Frame = new CGRect(0, Frame.Height/2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};


			Control.Layer.AddSublayer(_line);
			Control.TintColor = UIColor.Black;
		}

        private CGColor ColorToEntryLine()
		{
			return UIColor.FromRGB(229, 229, 229).CGColor;
		}
    }
}
