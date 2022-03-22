using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EshoPam.Mobile
{
    public partial class App : Application
    {
        private static string serviceBaseAddress;
        public static string ServiceBaseAddress => serviceBaseAddress;

        public App()
        {
            InitializeComponent();

            //#if DEBUG
            if (DeviceInfo.DeviceType == DeviceType.Virtual)
                serviceBaseAddress = "http://10.0.2.2:8081/api/";
            else
                serviceBaseAddress = "http://192.168.56.1:8081/api/";
            //#else
            //serviceBaseAddress = "https://eshopam.com/api/";
            //#endif

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
