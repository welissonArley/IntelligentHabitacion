using Xamarin.Forms;

namespace Homuai.App.ValueObjects
{
    public class AppColorTransformation : FFImageLoading.Transformations.TintTransformation
    {
        public AppColorTransformation()
        {
            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? "#000000" : "#FFFFFF";
            EnableSolidColor = true;
        }
    }
}
