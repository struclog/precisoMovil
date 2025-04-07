using Newtonsoft.Json;
using PrecisoGps.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrecisoGps.Services
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<SimCard>> GetSimCardsAsync()
        {
            string url = "https://precisogps.com/api/simcardsApi";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al consumir la API.");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SimCard>>(json);
        }
    }
}
