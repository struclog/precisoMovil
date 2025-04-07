using Newtonsoft.Json;
using PrecisoGps.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrecisoGps.Services
{
    public class ConductorService
    {
        private const string ApiUrl = "http://precisogps.com/api";

        private readonly HttpClient _httpClient;

        public ConductorService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Conductor>> ObtenerConductoresAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/conductores");

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var hojas = JsonConvert.DeserializeObject<List<Conductor>>(json);
                    return hojas;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener hojas: " + ex.Message);
            }
            return null; 

        }
    }
}
