using CoreAnimation;
using CoreGraphics;
using IntelligentHabitacion.App.CustomControl;
using IntelligentHabitacion.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryGrayBlackCursor), typeof(EntryGrayBlackCursorRenderer))]
namespace IntelligentHabitacion.App.iOS.CustomControl
{
    public class EntryGrayBlackCursorRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
			base.OnElementChanged(e);
			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;

			CALayer _line = new CALayer
			{
				BorderColor = GetLineColor(),
				BackgroundColor = GetLineColor(),
				Frame = new CGRect(0, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};


			Control.Layer.AddSublayer(_line);
			Control.TintColor = GetCursor();
		}

		private CGColor GetLineColor()
		{
			(int R, int G, int B) = Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? (98, 97, 94) : (229, 229, 229);

			return UIColor.FromRGB(R, G, B).CGColor;
		}

		private UIColor GetCursor()
		{
			return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.White : UIColor.Black;
		}
	}
}
