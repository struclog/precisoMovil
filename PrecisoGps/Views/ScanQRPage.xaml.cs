using Newtonsoft.Json;
using PrecisoGps.Responses;
using PrecisoGps.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrecisoGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRPage : ContentPage
    {
        public ScanQRPage()
        {
            InitializeComponent();
        }

        private async void OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                scannerView.IsAnalyzing = false;
                scannerView.IsScanning = false;

                try
                {
                    int unidadId = int.Parse(result.Text);

                    var service = new HojasTrabajoService();
                    var hoja = await service.ObtenerHojaPorUnidadAsync(unidadId);

                    if (hoja != null)
                    {
                        await Navigation.PushAsync(new GestionHojaPage(hoja));
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener o crear la hoja.", "OK");
                        scannerView.IsScanning = true;
                        scannerView.IsAnalyzing = true;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"QR inválido o error: {ex.Message}", "OK");
                    scannerView.IsScanning = true;
                    scannerView.IsAnalyzing = true;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            scannerView.IsScanning = true;
            scannerView.IsAnalyzing = true;
            IniciarAnimacionLinea();
        }

        protected override void OnDisappearing()
        {
            scannerView.IsScanning = false;
            scannerView.IsAnalyzing = false;
            base.OnDisappearing();
        }

        private async void IniciarAnimacionLinea()
        {
            double from = 0;
            double to = 198;

            lineaEscaneo.TranslationY = from;

            while (scannerView.IsScanning)
            {
                await lineaEscaneo.TranslateTo(0, to, 1000, Easing.Linear);
                await lineaEscaneo.TranslateTo(0, from, 1000, Easing.Linear);
            }
        }

    }
}
