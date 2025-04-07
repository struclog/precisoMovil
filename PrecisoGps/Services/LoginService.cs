using PrecisoGps.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrecisoGps.Services
{
    public class LoginService
    {
        private const string ApiUrl = "http://precisogps.com/api";

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var httpClient = new HttpClient();
            var payload = new { correo = email, clave = password };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            try
            {

                var response = await httpClient.PostAsync($"{ApiUrl}/auth", content);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var responseJs = JsonSerializer.Deserialize<LoginResponse>(json);
                    return JsonSerializer.Deserialize<LoginResponse>(json);
                }
            }
            catch (Exception ex)
            {

                return null;
            }

            return null;
        }
        public async Task<bool> ValidarToken(string token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{ApiUrl}/user"); // o cualquier endpoint protegido

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

    }

}
