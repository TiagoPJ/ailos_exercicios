using Newtonsoft.Json;
using Questao2.Interfaces;
using Questao2.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Questao2.Services
{
    public class HttpClientService : IHttpClientService
    {
        private const string UrlBase = "https://jsonmock.hackerrank.com/api/football_matches?year=";
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public MatchResponse BuscarInformacoesTime(string time, int ano)
        {
            // Efetuando uma requisição à API
            string url = string.Format("{0}{1}&team1={2}", UrlBase, ano, time);
            return buscarIformacoesNaAPI(url);
        }

        public MatchResponse BuscarInformacoesTimePorPagina(string time, int ano, int pagina)
        {
            // Efetuando uma requisição à API
            string url = string.Format("{0}{1}&team1={2}&page={3}", UrlBase, ano, time, pagina);
            return buscarIformacoesNaAPI(url);
        }

        private MatchResponse buscarIformacoesNaAPI(string url)
        {
            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            // Deserializando a resposta da API
            return JsonConvert.DeserializeObject<MatchResponse>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
