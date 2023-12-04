using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.LoginAppService
{
    public class LoginAppService : ILoginAppService
    {
        private readonly string _apiUrl; 

        public LoginAppService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(_apiUrl, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return false;
            }
        }
    }
}
