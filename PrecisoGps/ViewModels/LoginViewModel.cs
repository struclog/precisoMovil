using Newtonsoft.Json;
using PrecisoGps.Models;
using PrecisoGps.Services;
using PrecisoGps.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private bool _isBusy;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Por favor, ingrese sus credenciales.", "OK");
                    return;
                }

                // Consumir servicio de inicio de sesión
                var loginService = new LoginService();
                var response = await loginService.LoginAsync(Email, Password);

                if (response != null && !string.IsNullOrEmpty(response.token))
                {
                    //await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión correcto.", "OK");
                    await SecureStorage.SetAsync("user_token", response.token);
                    await SecureStorage.SetAsync("user_perfil", response.user.perfil); // si lo tienes del backend

                    await Application.Current.MainPage.Navigation.PushAsync(new MenuPrincipal());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Credenciales incorrectas.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
