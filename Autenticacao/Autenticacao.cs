using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace CRMEducacional.Retencao.Teste.Web.Autenticacao
{
    public class Autenticacao
    {
        public TokenAutenticacao GeraTokenAutenticacao(string usuario, string senha)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "username", usuario }, { "password",  senha }, { "grant_type", "password" }
            });

            Task<HttpResponseMessage> message = client.PostAsync("http://localhost/RetencaoGalho//api//token", form);
            string result = message.Result.Content.ReadAsStringAsync().Result;
            if (message.Result.StatusCode == HttpStatusCode.OK)
            {
                var tokenAutenticacao = JsonConvert.DeserializeObject<TokenAutenticacao>(result);
                return tokenAutenticacao;
            }
            else

                return null;

        }

        public TokenAutenticacao RefreshTokenAutenticacao(string token_refresh)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "refresh_token", token_refresh }, { "grant_type", "refresh_token" }
            });

            Task<HttpResponseMessage> message = client.PostAsync("http://localhost/RetencaoGalho//api//token", form);
            string result = message.Result.Content.ReadAsStringAsync().Result;
            if (message.Result.StatusCode == HttpStatusCode.OK)
            {
                var tokenAutenticacao = JsonConvert.DeserializeObject<TokenAutenticacao>(result);
                return tokenAutenticacao;
            }
            else
                return null;

        }
    }

    public class TokenAutenticacao
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string username { get; set; }
        public string created_on { get; set; }
        public string expires_on { get; set; }
    }
}