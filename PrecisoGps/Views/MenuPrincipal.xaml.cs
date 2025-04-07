using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrecisoGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public static bool UsuarioLogueado { get; set; } = false;
        public static string PerfilUsuario { get; set; } = null;

        public MenuPrincipal()
        {
            InitializeComponent();
        }


        private async void OnIniciarSesionClicked(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();
            loginPage.Disappearing += async (s, args) =>
            {
                // Recuperar token y perfil después del login
                string token = await SecureStorage.GetAsync("user_token");
                string perfil = await SecureStorage.GetAsync("user_perfil");

                if (!string.IsNullOrEmpty(token) && perfil == "admin")
                {
                    UsuarioLogueado = true;
                    PerfilUsuario = perfil;

                    await DisplayAlert("Bienvenido", "Inicio de sesión exitoso", "OK");
                }
                else
                {
                    await DisplayAlert("Acceso restringido", "Debes tener perfil admin", "OK");
                }
            };

            await Navigation.PushAsync(loginPage);
        }

        private async void OnAsignacionConductorClicked(object sender, System.EventArgs e)
        {
           
        }

        private async void OnContadoresClicked(object sender, System.EventArgs e)
        {
            if (!UsuarioLogueado || PerfilUsuario != "admin")
            {
                await DisplayAlert("Acceso denegado", "Debes iniciar sesión como admin", "OK");
                return;
            }

            await Navigation.PushAsync(new HojasTrabajoPage());
            // await Navigation.PushAsync(new ContadoresPage());
        }

        private async void OnMantenimientoPreventivoClicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new MantenimientoPreventivoPage());
        }

        private async void OnMantenimientoCorrectivoClicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new MantenimientoCorrectivoPage());
        }
        private async void OnCrearReporteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanQRPage());
        }

    }
}