using PrecisoGps.Services;
using PrecisoGps.Views;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrecisoGps
{
    public partial class App : Application
    {
        public static string Token { get; set; }

        public App()
        {

            InitializeComponent();

            MainPage = new NavigationPage(new MenuPrincipal());
        }

        protected async override void OnStart()
        {
            //string token = await SecureStorage.GetAsync("user_token");
            //if (!string.IsNullOrEmpty(token))
            //{
            //    var isValid = await new LoginService().ValidarToken(token);

            //    if (isValid)
            //    {
            //        MainPage = new NavigationPage(new MenuPrincipal());
            //    }
            //    else
            //    {
            //        MainPage = new NavigationPage(new LoginPage());
            //    }
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
