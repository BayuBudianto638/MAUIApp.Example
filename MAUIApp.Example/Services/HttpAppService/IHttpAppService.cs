using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.HttpAppService
{
    public interface IHttpAppService: IDisposable
    {
        Task<HttpResponseMessage> PostAsync(string url, string content);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
