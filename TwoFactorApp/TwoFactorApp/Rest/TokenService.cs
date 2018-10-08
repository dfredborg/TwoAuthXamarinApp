using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwoFactorApp.models;

namespace TwoFactorApp.Rest
{
    public class TokenService
    {
        private const string url = "http://10.100.8.43/masterservice/api/TwoFactor/GetToken?token=";
        private HttpClient _Client = new HttpClient();

        public async Task<TwoFactor> GetToken(string SecretToken)
        {
            TwoFactor twoFactor = new TwoFactor();

            try
            {
                var content = await _Client.GetStringAsync(url+SecretToken);
                twoFactor = JsonConvert.DeserializeObject<TwoFactor>(content);
            }
            catch (Exception e)
            {
                twoFactor.LoginToken = "Your Secret token " +SecretToken +" is not valid";
            }            

            return twoFactor;
        }
    }
}
