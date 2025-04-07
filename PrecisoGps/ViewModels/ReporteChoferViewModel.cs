using Newtonsoft.Json;
using PrecisoGps.Responses;
using PrecisoGps.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class ReporteChoferViewModel : BaseViewModel
    {
        private HojasTrabajoService _service = new HojasTrabajoService();

        public ResposeHoja Hoja { get; set; }
        public Command GuardarCommand { get; }

        public ReporteChoferViewModel(ResposeHoja hoja)
        {
            Hoja = hoja;
            GuardarCommand = new Command(async () => await GuardarProduccionAsync());
        }

        private async Task GuardarProduccionAsync()
        {
            //var resultado = await _service.ActualizarProduccionHojaAsync(Hoja.id_hoja, Hoja.producciones);
            //if (resultado)
            //    await Application.Current.MainPage.DisplayAlert("Éxito", "Producción guardada", "OK");
            //else
            //    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar", "OK");
        }
    }
}
