using OneOf;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Utils;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        void Iniciar();
        Task<AccountDataResponse> GetAccountByIdAsync(string id);
        Task<IEnumerable<MovimentDataResponse>> GetMovimentsByIdAccount(string id);
        Task<OneOf<Result<MovimentDataResponse>, ErrorResult>> AddMovimentAsync(MovimentDataRequest command);
    }
}
