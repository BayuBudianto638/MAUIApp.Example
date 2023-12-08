using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.Sessions
{
    public static class SessionAppService
    {
        private static string? TokenKey { get; set; }

        public static string GetToken()
        {
            try
            {
                string token = SecureStorage.GetAsync(TokenKey).Result;
                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SetToken(string token)
        {
            try
            {
                SecureStorage.SetAsync(TokenKey, token);
            }
            catch (Exception ex)
            {

            }
        }

        public static void ClearToken()
        {
            try
            {
                SecureStorage.Remove(TokenKey);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
