using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questao2.Objetos;

namespace Questao2.Interfaces
{
    public interface IHttpClientService
    {
        MatchResponse BuscarInformacoesTime(string time, int ano);
        MatchResponse BuscarInformacoesTimePorPagina(string time, int ano, int pagina);
    }
}
