using Questao2.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2.Interfaces
{
    public interface IMatchService
    {
        TimeDomain RetornarTotalGols(string time, int ano);
    }
}
