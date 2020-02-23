﻿using Android.Content;
using Android.OS;
using IntelligentHabitacion.App.CustomControl;
using IntelligentHabitacion.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelJustifyText), typeof(LabelJustifyTextRenderer))]
namespace IntelligentHabitacion.App.Droid.CustomControl
{
    public class LabelJustifyTextRenderer : LabelRenderer
    {
        public LabelJustifyTextRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.OMr1)
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
                else
                    Control.TextAlignment = Android.Views.TextAlignment.Center;
            }
        }
    }
}