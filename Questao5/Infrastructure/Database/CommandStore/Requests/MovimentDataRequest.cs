namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentDataRequest
    {
        public string IdMovimento => Guid.NewGuid().ToString();
        public string IdContaCorrente { get; set; }
        public string DataMovimento => DateTime.Now.ToShortDateString();
        public string TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
