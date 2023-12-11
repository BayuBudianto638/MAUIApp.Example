using MAUIApp.Example.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.LoginAppService
{
    public class LoginAppService : ILoginAppService
    {
        public LoginAppService()
        {

        }

        public async Task<(bool, string)> LoginAsync(string username, string password)
        {
            string tokenValue = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent($"{{\"userName\":\"{username}\",\"password\":\"{password}\",\"email\":\"Shiawase@gmail.com\"}}", Encoding.UTF8, "application/json");

                    var _apiUrl = $"{AppSettings.ApiUrl}api/Login";
                    HttpResponseMessage response = await client.PostAsync(_apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<Tokens>(jsonResponse);
                        tokenValue = responseObject.Token;
                    }

                    return (response.IsSuccessStatusCode, tokenValue);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error during login: {ex.Message}");
            }
        }
    }
}
