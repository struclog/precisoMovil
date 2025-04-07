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
            VerificarSesionGuardada();
        }

        private async void VerificarSesionGuardada()
        {
            string token = await SecureStorage.GetAsync("user_token");
            string perfil = await SecureStorage.GetAsync("user_perfil");

            if (!string.IsNullOrEmpty(token))
            {
                var loginService = new Services.LoginService();
                bool isValid = await loginService.ValidarToken(token);

                if (isValid && perfil == "admin")
                {
                    UsuarioLogueado = true;
                    PerfilUsuario = perfil;
                }
            }
        }

        private async void OnAsignacionConductorClicked(object sender, System.EventArgs e)
        {
            if (!UsuarioLogueado || PerfilUsuario != "admin")
            {
                await DisplayAlert("Acceso denegado", "Debes iniciar sesión como admin", "OK");
                await MostrarLoginPage();
                return;
            }

            // Aquí puedes navegar a la página de conductores cuando esté disponible
            await DisplayAlert("Información", "Funcionalidad en desarrollo", "OK");
        }

        private async void OnContadoresClicked(object sender, System.EventArgs e)
        {
            if (!UsuarioLogueado || PerfilUsuario != "admin")
            {
                await DisplayAlert("Acceso denegado", "Debes iniciar sesión como admin", "OK");
                await MostrarLoginPage();
                return;
            }

            await Navigation.PushAsync(new HojasTrabajoPage());
        }

        private async void OnCrearReporteClicked(object sender, EventArgs e)
        {
            if (!UsuarioLogueado || PerfilUsuario != "admin")
            {
                await DisplayAlert("Acceso denegado", "Debes iniciar sesión como admin", "OK");
                await MostrarLoginPage();
                return;
            }

            await Navigation.PushAsync(new ScanQRPage());
        }

        // Método auxiliar para mostrar la página de login
        private async Task MostrarLoginPage()
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
                }
            };

            await Navigation.PushAsync(loginPage);
        }

        // Mantenemos estos métodos por compatibilidad
        private async void OnMantenimientoPreventivoClicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new MantenimientoPreventivoPage());
        }

        private async void OnMantenimientoCorrectivoClicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new MantenimientoCorrectivoPage());
        }
    }
}