using PrecisoGps.Responses;
using PrecisoGps.ViewModels;
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
    public partial class GestionHojaPage : ContentPage
    {
        public GestionHojaPage(ResposeHoja hoja)
        {
            InitializeComponent();
            var vm = (GestionHojaViewModel)this.BindingContext;
            vm.CargarDatos(hoja);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var photoStatus = await Permissions.CheckStatusAsync<Permissions.Media>();
            if (photoStatus != PermissionStatus.Granted)
            {
                photoStatus = await Permissions.RequestAsync<Permissions.Media>();
            }

            if (photoStatus != PermissionStatus.Granted)
            {
                await DisplayAlert("Permiso requerido", "Debes permitir el acceso a fotos para subir imágenes.", "OK");
            }
        }

    }
}