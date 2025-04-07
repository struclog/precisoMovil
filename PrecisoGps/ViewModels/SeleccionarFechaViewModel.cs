using PrecisoGps.Responses;
using PrecisoGps.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class SeleccionarFechaViewModel : BaseViewModel
    {
        public DateTime FechaSeleccionada { get; set; } = DateTime.Today;

        public ICommand BuscarCommand { get; }

        private readonly HojasTrabajoService _service;

        public SeleccionarFechaViewModel()
        {
            _service = new HojasTrabajoService();
            BuscarCommand = new Command(async () => await BuscarHojasAsync());
        }

        private async Task BuscarHojasAsync()
        {
            var fecha = FechaSeleccionada.ToString("yyyy-MM-dd");
            var hojas = await _service.ObtenerHojasPorFechaAsync(fecha);

            if (hojas != null)
            {
                // Aquí puedes navegar a otra página y mostrar los resultados
                await Application.Current.MainPage.DisplayAlert("Éxito", $"Se encontraron {hojas.Count} hojas.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener las hojas.", "OK");
            }
        }
    }
}
