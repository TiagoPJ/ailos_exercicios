namespace Questao5.Application.Queries.Responses
{
    public class FindAccountByIdResponse
    {
        public int NumeroConta { get; set; }
        public string Nome { get; set; }
        public DateTime DataSolicitacao { get => DateTime.Now.Date; }
        public decimal Saldo { get; set; }
    }
}
