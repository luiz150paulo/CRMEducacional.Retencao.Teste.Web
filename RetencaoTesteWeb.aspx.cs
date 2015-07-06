using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CRMEducacional.Retencao.Teste.Web.Autenticacao;
using CRMEducacional.Retencao.Teste.Web.Classes;
using Newtonsoft.Json;

namespace CRMEducacional.Retencao.Teste.Web
{
    public partial class RetencaoTesteWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TokenAutenticacao tokenAutenticacao = GerarTokenAutenticacao("iago.moura@crmeducacional0", "C@dsoft1");

            AtualizarSGA(tokenAutenticacao.token_type, tokenAutenticacao.access_token);
        }

        private void AtualizarSGA(string token, string type_token)
        {
            string entidade = Request.Form["Entidade"];

            if (!string.IsNullOrEmpty(entidade))
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token, type_token);

                Task<HttpResponseMessage> messageGet = client.GetAsync("http://localhost/RetencaoGalho//api/" + entidade);
                if (messageGet.Result.IsSuccessStatusCode)
                {
                    var retorno = messageGet.Result.Content.ReadAsStringAsync();

                    List<Contato> contatos = JsonConvert.DeserializeObject<List<Contato>>(retorno.Result, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                    //List<Contato> contatos = JsonConvert.DeserializeObject<List<Contato>>(retorno.Result);
                    foreach (var contato in contatos)
                    {
                        //Aqui você deve atualizar os contatos na base de dados do seu SGA.
                    }


                    //Informando ao Retenção que a atualização foi realizada com sucesso.
                    var listaContatosAtualizados = contatos.Select(contato => new ConfirmacaoContatosAtualizadosVO
                    {
                        ChaveIntegracaoSGA = contato.CPF,
                        IdentificadorFila = contato.IdentificadorFila
                    }).ToList();


                    var messagePost = client.PostAsync("http://localhost/RetencaoGalho//api/ContatosAtualizados",
                        new StringContent(
                            JsonConvert.SerializeObject(listaContatosAtualizados), Encoding.UTF8, "application/json"));

                    if (messagePost.Result.StatusCode == HttpStatusCode.OK)
                    {
                        //Confirmado com sucesso
                    }
                    else
                    {
                        //Falha
                        //messagePost.Result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
        }

        private TokenAutenticacao GerarTokenAutenticacao(string usuario, string senha)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> {
		        {
			        "username", usuario
		        }, {
			        "password", senha
		        }, {
			        "grant_type", "password"
		        }
	        });

            Task<HttpResponseMessage> message = client.PostAsync("http://localhost//RetencaoGalho//api//token", form);
            string result = message.Result.Content.ReadAsStringAsync().Result;
            if (message.Result.StatusCode == HttpStatusCode.OK)
            {
                var tokenAutenticacao = JsonConvert.DeserializeObject<TokenAutenticacao>(result);
                return tokenAutenticacao;
            }
            return null;
        }
    }
}