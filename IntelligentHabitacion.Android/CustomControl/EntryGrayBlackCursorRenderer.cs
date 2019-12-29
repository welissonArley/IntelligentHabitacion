using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using IntelligentHabitacion.CustomControl;
using IntelligentHabitacion.Droid.CustomControl;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryGrayBlackCursor), typeof(EntryGrayBlackCursorRenderer))]
namespace IntelligentHabitacion.Droid.CustomControl
{
    public class EntryGrayBlackCursorRenderer : EntryRenderer
    {
        public EntryGrayBlackCursorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.ParseColor("#E5E5E5"));
                else
                    Control.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#E5E5E5"), PorterDuff.Mode.SrcAtop);

                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.CursorEntryBlack);
            }
        }
    }
}