using Android.Content;
using IntelligentHabitacion.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Frame), typeof(AppFrameRenderer))]
namespace IntelligentHabitacion.App.Droid.CustomControl
{
    public class AppFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public AppFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement.HasShadow)
            {
                var color = GetShadowColor();
                SetOutlineSpotShadowColor(color);
                Elevation = 5.0f;
                TranslationZ = 10.0f;
            }
        }

        private Android.Graphics.Color GetShadowColor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.White : Android.Graphics.Color.Black;
        }
    }
}