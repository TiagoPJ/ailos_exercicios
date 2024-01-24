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
    public class MatchService : IMatchService
    {
        private readonly IHttpClientService _httpClientService;
        public MatchService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public TimeDomain RetornarTotalGols(string time, int ano)
        {
            MatchResponse matchResponse = _httpClientService.BuscarInformacoesTime(time, ano);
            int totalGols = 0;
            for (int i = 0; i < matchResponse.total_pages; i++)
            {
                totalGols += _httpClientService.BuscarInformacoesTimePorPagina(time, ano, i).data.Sum(x => int.Parse(x.team1goals));
            }
            bool timeExiste = matchResponse.data.Count() > 0;
            return new TimeDomain(time, totalGols, ano, timeExiste);
        }

        //public void BuscarInformacoesTimePorPagina(int page, string time)
        //{

        //}

        //// Criando uma instância do IServiceCollection
        //IServiceCollection services = new ServiceCollection();

        //// Registrando um serviço
        //services.AddHttpClient();

        //    // Criando uma instância do IServiceProvider
        //    IServiceProvider serviceProvider = services.BuildServiceProvider();

        //// Obtendo o serviço HttpClient
        //HttpClient client = serviceProvider.GetService<HttpClient>();

        //// Efetuando uma requisição à API
        //HttpResponseMessage response = client.GetAsync("https://jsonmock.hackerrank.com/api/football_matches").Result;

        //// Deserializando a resposta da API
        //List<MatchResponse> matches = JsonConvert.DeserializeObject<List<MatchResponse>>(response.Content.ReadAsStringAsync().Result);

    }
}
