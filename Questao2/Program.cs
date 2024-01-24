using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Questao2;
using Questao2.Interfaces;
using Questao2.Services;
using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    private static int ANO = 2016; // API só consta dados até 2016
    public static void Main()
    {
        IServiceCollection services = registraServicos();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        // Obtendo o serviço HttpClient
        IMatchService matchService = serviceProvider.GetService<IMatchService>();

        char resp = validarInformacaoInicial();
        if (resp.Equals('s'))
        {
            string time = validarExistentenciaTime(matchService);
            int ano = obterAnoValido();
            var timeDados = matchService.RetornarTotalGols(time, ano);
            if (timeDados.Ano == ANO)
            {
                Console.WriteLine(timeDados.ToString());
                Console.WriteLine();
            }
            else
            {
                var timeGolsProximoAno = matchService.RetornarTotalGols(time, ano + 1);
                string totalGolsTemporada = string.Format("A temporada na Europa começa na metade do ano, portanto o Paris Saint_Germain marcou {0} na temporada {1}/{2}.", timeDados.TotalGols + timeGolsProximoAno.TotalGols, ano, ano + 1);
                Console.WriteLine(totalGolsTemporada);
                Console.WriteLine(timeDados.ToString());
                Console.WriteLine(timeGolsProximoAno.ToString());
                Console.WriteLine();
            }
        }
        else
        {
            var timeGolsParis2014 = matchService.RetornarTotalGols("paris saint-germain", 2014);
            var timeGolsParis2015 = matchService.RetornarTotalGols("paris saint-germain", 2015);
            string totalGolsTemporadaParis = string.Format("A temporada na Europa começa na metade do ano, portanto o Paris Saint_Germain marcou {0} na temporada 2014/2015.", timeGolsParis2014.TotalGols + timeGolsParis2015.TotalGols);
            Console.WriteLine(totalGolsTemporadaParis);
            Console.WriteLine(timeGolsParis2014.ToString());
            Console.WriteLine(timeGolsParis2015.ToString());
            Console.WriteLine();

            var timeGolsChelsea2014 = matchService.RetornarTotalGols("chelsea", 2014);
            var timeGolsChelsea2015 = matchService.RetornarTotalGols("chelsea", 2015);
            string totalGolsTemporadaChelsea = string.Format("A temporada na Europa começa na metade do ano, portanto o Chelsea marcou {0} na temporada 2014/2015.", timeGolsChelsea2014.TotalGols + timeGolsChelsea2015.TotalGols);
            Console.WriteLine(totalGolsTemporadaChelsea);
            Console.WriteLine(timeGolsChelsea2014.ToString());
            Console.WriteLine(timeGolsChelsea2015.ToString());
        }

        //string teamName = "Paris Saint-Germain";
        //int year = 2013;
        //int totalGoals = getTotalScoredGoals(teamName, year);

        //Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        //teamName = "Chelsea";
        //year = 2014;
        //totalGoals = getTotalScoredGoals(teamName, year);

        //Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static int obterAnoValido()
    {
        Console.WriteLine(string.Format("OBS.: O ano a ser informado deve ser menor ou igual {0}. ", ANO));
        Console.WriteLine("Entre o ano da pesquisa: ");
        int ano;
        if (int.TryParse(Console.ReadLine(), out ano) && ano <= ANO)
            return ano;
        else
        {
            Console.Clear();
            Console.WriteLine("Você deve digitar apenas números.");
            return obterAnoValido();
        }
    }

    private static string validarExistentenciaTime(IMatchService matchService)
    {
        Console.WriteLine("Digite o time a ser pesquisado: ");
        string time = Console.ReadLine();
        if (matchService.RetornarTotalGols(time, ANO).ExisteDados)
            return time;
        else
        {
            Console.Clear();
            Console.WriteLine("Dados não encontrados.");
            return validarExistentenciaTime(matchService);
        }
    }

    private static char validarInformacaoInicial()
    {
        Console.WriteLine("Você gostaria de escolher o time, caso não retornará opções default, digite (s/n)? ");
        char resp;
        if (char.TryParse(Console.ReadLine(), out resp) &&
            (resp.ToLower(CultureInfo.InvariantCulture).Equals('s') ||
             resp.ToLower(CultureInfo.InvariantCulture).Equals('n')))
            return resp.ToLower(CultureInfo.InvariantCulture);
        else
        {
            Console.Clear();
            Console.WriteLine("Valor digitado não corresponde com o esperado.");
            return validarInformacaoInicial();
        }
    }

    private static IServiceCollection registraServicos()
    {
        // Criando uma instância do IServiceCollection
        IServiceCollection services = new ServiceCollection();
        services.AddHttpClient();
        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IMatchService, MatchService>();

        return services;
    }
}