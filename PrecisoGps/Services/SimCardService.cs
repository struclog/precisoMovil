using Newtonsoft.Json;
using PrecisoGps.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrecisoGps.Services
{
    public class SimCardService
    {
        private readonly HttpClient _httpClient;

        public SimCardService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<SimCard>> GetSimCardsAsync(string id)
        {

            var data = Uri.EscapeDataString(id);    
            var url = $"https://precisogps.com/api/simcards?search={data}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SimCard>>(jsonResponse);
            }

            return new List<SimCard>();

        }



        public async Task<bool> UpdateSimCardAsync(SimCard simCard)
        {
            var url = $"https://precisogps.com/api/simcards/{simCard.ID_SIM}";
            var json = JsonConvert.SerializeObject(simCard);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            return response.IsSuccessStatusCode;
        }

    }
}
