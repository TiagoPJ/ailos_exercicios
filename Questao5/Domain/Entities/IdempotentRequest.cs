namespace Questao5.Domain.Entities
{
    public class IdempotentRequest
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
