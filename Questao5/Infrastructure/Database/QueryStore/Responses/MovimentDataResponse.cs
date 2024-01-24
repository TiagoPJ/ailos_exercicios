using Questao5.Domain.Entities;
using System.Drawing;
using System;
using System.ComponentModel;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class MovimentDataResponse
    {
        [Description("IdMovimento")]
        public string Id { get; set; }
        public string IdContaCorrente { get; set; }

        [Description("DataMovimento")]
        public string Data { get; set; }

        [Description("TipoMovimento")]
        public char Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
