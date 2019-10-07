using Xamarin.Forms;

namespace XamarinAppCsharp
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage()){
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White
            };
        }
    }
}
