using CoreAnimation;
using CoreGraphics;
using IntelligentHabitacion.App.CustomControl;
using IntelligentHabitacion.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryTransparentBlackCursor), typeof(EntryTransparentBlackCursorRenderer))]
namespace IntelligentHabitacion.App.iOS.CustomControl
{
	public class EntryTransparentBlackCursorRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;

			CALayer _line = new CALayer
			{
				BorderColor = ColorToEntryLine(),
				BackgroundColor = ColorToEntryLine(),
				Frame = new CGRect(0, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};

			Control.Layer.AddSublayer(_line);
			Control.TintColor = UIColor.Black;
		}

		private CGColor ColorToEntryLine()
		{
			return UIColor.Clear.CGColor;
		}
	}
}