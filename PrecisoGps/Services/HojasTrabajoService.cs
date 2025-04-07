using Newtonsoft.Json;
using PrecisoGps.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PrecisoGps.Services
{
    internal class HojasTrabajoService
    {
        private const string ApiUrl = "http://precisogps.com/api";

        private readonly HttpClient _httpClient;

        public HojasTrabajoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ResposeHoja>> ObtenerHojasPorFechaAsync(string fecha)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrl}/hojas?fecha={fecha}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var hojas = JsonConvert.DeserializeObject<List<ResposeHoja>>(json);
                    return hojas;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener hojas: " + ex.Message);
            }

            return null;
        }

        public async Task<ResposeHoja> ObtenerHojaPorUnidadAsync(int idUnidad)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/hoja-chofer/{idUnidad}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResposeHoja>(json);
            }
            return null;
        }

        public async Task<bool> ActualizarProduccionHojaAsync(
            int id_hoja,
            ObservableCollection<Produccione> produccion,
            ObservableCollection<Gasto> gastos,
            int id_conductor,
            int id_ruta,
            string ayudante,
            string fecha,
            string tipoDia,
            int id_unidad
        )
        {
            var gastosPayload = gastos.Select(g => new
            {
                tipo_gasto = g.tipo_gasto,
                valor = double.TryParse(g.valor, out var v) ? v : 0.0,
                imagen_base64 = (!string.IsNullOrWhiteSpace(g.imagen)
                                 && (g.tipo_gasto == "DIESEL" || g.tipo_gasto == "OTROS")
                                 && !g.imagen.StartsWith("gastos/") && !g.imagen.StartsWith("http"))
                                ? $"data:image/png;base64,{g.imagen}"
                                : null
            });

            var produccionPayload = produccion.Select(p => new
            {
                p.nro_vuelta,
                p.hora_subida,
                p.hora_bajada,
                valor_vuelta = double.TryParse(p.valor_vuelta, out var vv) ? vv : 0.0
            });

            var payload = new
            {
                fecha = fecha,
                tipo_dia = tipoDia,
                id_conductor,
                ayudante_nombre = ayudante,
                id_ruta,
                id_unidad,
                gastos = gastosPayload,
                produccion = produccionPayload
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{ApiUrl}/hojas/{id_hoja}", content);

            string responseContent = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<byte[]> DescargarPdfAsync(int idHoja)
        {
            string token = await SecureStorage.GetAsync("user_token");
            if (string.IsNullOrEmpty(token)) throw new Exception("Token no disponible");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"http://precisogps.com/api/hojas-trabajo/{idHoja}/generar-pdf"; // Reemplaza con tu URL real

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo descargar el PDF");

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
