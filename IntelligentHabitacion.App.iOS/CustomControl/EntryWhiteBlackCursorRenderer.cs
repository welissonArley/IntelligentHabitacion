using CoreAnimation;
using CoreGraphics;
using IntelligentHabitacion.App.CustomControl;
using IntelligentHabitacion.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryWhiteBlackCursor), typeof(EntryWhiteBlackCursorRenderer))]
namespace IntelligentHabitacion.App.iOS.CustomControl
{
	public class EntryWhiteBlackCursorRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
			base.OnElementChanged(e);
			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;
            Control.TextAlignment = UITextAlignment.Center;

			CALayer _line = new CALayer
			{
				BorderColor = UIColor.White.CGColor,
				BackgroundColor = UIColor.White.CGColor,
				Frame = new CGRect(0, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};

			Control.Layer.AddSublayer(_line);
			Control.TintColor = UIColor.Black;
		}
    }
}
