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

[assembly: ExportRenderer(typeof(EntryTransparentBlackCursor), typeof(EntryTransparentBlackCursorRenderer))]
namespace IntelligentHabitacion.App.Droid.CustomControl
{
    public class EntryTransparentBlackCursorRenderer : EntryRenderer
    {
        public EntryTransparentBlackCursorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.Background.SetColorFilter(new BlendModeColorFilter(Android.Graphics.Color.Transparent, BlendMode.SrcAtop));
                    Control.SetTextCursorDrawable(Resource.Drawable.CursorEntryBlack);
                }
                else
                {
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.CursorEntryBlack);
                }
            }
        }
    }
}