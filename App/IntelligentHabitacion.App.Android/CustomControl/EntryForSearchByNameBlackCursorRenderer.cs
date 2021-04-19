using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using IntelligentHabitacion.App.CustomControl;
using IntelligentHabitacion.App.Droid.CustomControl;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryForSearchByNameBlackCursor), typeof(EntryForSearchByNameBlackCursorRenderer))]
namespace IntelligentHabitacion.App.Droid.CustomControl
{
    public class EntryForSearchByNameBlackCursorRenderer : EntryRenderer
    {
        public EntryForSearchByNameBlackCursorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.Background.SetColorFilter(new BlendModeColorFilter(Android.Graphics.Color.Rgb(241, 241, 241), BlendMode.SrcAtop));
                    Control.SetTextCursorDrawable(Resource.Drawable.CursorEntryBlack);
                }
                else
                {
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Rgb(241,241,241));
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.CursorEntryBlack);
                }
            }
        }
    }
}