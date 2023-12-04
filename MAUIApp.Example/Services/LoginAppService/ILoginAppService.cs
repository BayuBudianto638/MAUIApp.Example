using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.LoginAppService
{
    public interface ILoginAppService
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
