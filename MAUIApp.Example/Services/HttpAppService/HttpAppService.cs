using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.HttpAppService
{
    public class HttpAppService: IHttpAppService
    {
        private readonly HttpClient _httpClient;

        public HttpAppService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string content)
        {
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, stringContent);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
