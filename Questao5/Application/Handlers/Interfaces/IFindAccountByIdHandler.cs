using OneOf;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Utils;

namespace Questao5.Application.Handlers.Interfaces
{
    public interface IFindAccountByIdHandler
    {
        Task<OneOf<Result<FindAccountByIdResponse>, ErrorResult>> Handle(FindAccountByIdRequest command);
    }
}
