using System.ComponentModel;

namespace Questao5.Domain.Entities
{
    public class Moviment
    {
        public Moviment() { }
        public Moviment(string idContaCorrente, string tipo, decimal valor)
        {
            IdMovimento = new Guid().ToString();
            IdContaCorrente = idContaCorrente;
            DataMovimento = DateTime.Now.ToShortDateString();
            Tipo = tipo;
            Valor = valor;
        }

        [Description("IdMovimento")]
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        [Description("TipoMovimento")]
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
