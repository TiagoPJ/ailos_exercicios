using MediatR;
using OneOf;
using Questao5.Application.Commands.Responses;
using Questao5.Utils;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovimentRequest : IRequest<OneOf<Result<CreateMovimentResponse>, ErrorResult>>
    {
        public string IdContaCorrente { get; set; }
        private string _tipo;
        public string Tipo
        {
            set => _tipo = value?.Trim().ToUpper();
            get => _tipo;
        }
        public decimal Valor { get; set; }
    }
}
