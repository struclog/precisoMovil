using PrecisoGps.Responses;
using PrecisoGps.Services;
using PrecisoGps.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class HojasTrabajoViewModel : BaseViewModel
    {
        // Definir la propiedad IsRefreshing para el RefreshView
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        // Agregar una propiedad para el indicador de ocupado si no existe en la clase base
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public Command RefreshCommand { get; }
        public Command<ResposeHoja> GenerarPdfCommand { get; }
        public ObservableCollection<ResposeHoja> Hojas { get; set; } = new ObservableCollection<ResposeHoja>();

        private DateTime _fechaSeleccionada = DateTime.Today;
        public DateTime FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set => SetProperty(ref _fechaSeleccionada, value);
        }

        public Command BuscarCommand { get; }
        public Command<ResposeHoja> VerDetallesCommand { get; }
        public Command NuevaHojaCommand { get; }

        public HojasTrabajoViewModel()
        {
            BuscarCommand = new Command(async () => await BuscarAsync());
            VerDetallesCommand = new Command<ResposeHoja>(VerDetalles);
            GenerarPdfCommand = new Command<ResposeHoja>(GenerarPdf);
            // NuevaHojaCommand = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new CrearHojaPage()));

            // Agregar el RefreshCommand
            RefreshCommand = new Command(async () => await RefreshAsync());
        }

        // Método para manejar la actualización por deslizar hacia abajo
        private async Task RefreshAsync()
        {
            try
            {
                // La propiedad IsRefreshing ya debe estar en true porque el control RefreshView 
                // la establece cuando el usuario desliza hacia abajo
                await BuscarAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar: {ex.Message}", "OK");
            }
            finally
            {
                // Crucial: Asegúrate de que IsRefreshing se establezca en false
                IsRefreshing = false;
            }
        }

        private async Task BuscarAsync()
        {
            // No usamos IsBusy para evitar errores si no está en la clase base
            try
            {
                var servicio = new HojasTrabajoService();
                var fecha = FechaSeleccionada.ToString("yyyy-MM-dd");
                var resultado = await servicio.ObtenerHojasPorFechaAsync(fecha);
                Hojas.Clear();
                if (resultado != null && resultado.Count > 0)
                {
                    foreach (var hoja in resultado)
                        Hojas.Add(hoja);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No se encontraron hojas para esa fecha", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void VerDetalles(ResposeHoja hoja)
        {
            if (hoja == null) return;
            //await Application.Current.MainPage.Navigation.PushAsync(new EditarHojaPage(hoja));
        }

        private async void GenerarPdf(ResposeHoja hoja)
        {
            try
            {
                if (hoja == null) return;
                var servicio = new HojasTrabajoService();
                var pdfBytes = await servicio.DescargarPdfAsync(hoja.id_hoja);
                string fileName = $"hoja_{hoja.id_hoja}.pdf";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
                File.WriteAllBytes(filePath, pdfBytes);
                await Application.Current.MainPage.DisplayAlert("PDF generado", $"Archivo guardado en:\n{filePath}", "OK");
                // Opcional: abrir el archivo
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}