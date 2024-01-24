using MediatR;
using OneOf;
using Questao5.Application.Queries.Responses;
using Questao5.Utils;

namespace Questao5.Application.Queries.Requests
{
    public class FindAccountByIdRequest : IRequest<OneOf<Result<FindAccountByIdResponse>, ErrorResult>>
    {
        public string Id { get; set; }        
    }
}
