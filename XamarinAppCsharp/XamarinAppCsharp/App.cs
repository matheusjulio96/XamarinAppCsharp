using Xamarin.Forms;
using XamarinAppCsharp.Data;

namespace XamarinAppCsharp
{
    public class App : Application
    {
        public static TimezoneManager TimezoneManager { get; private set; }
        public App()
        {
            TimezoneManager = new TimezoneManager(new TimezoneService());

            MainPage = new NavigationPage(new MainPage()){
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White
            };
        }
    }
}
