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
        private CALayer _line;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
			base.OnElementChanged(e);
			_line = null;

			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;
            Control.TextAlignment = UITextAlignment.Center;

			_line = new CALayer
			{
                BorderColor = UIColor.White.CGColor,
                BackgroundColor = UIColor.White.CGColor,
                Frame = new CGRect(0, Frame.Height/2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};


			Control.Layer.AddSublayer(_line);
			Control.TintColor = UIColor.Black;
		}
    }
}
