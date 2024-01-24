using AutoMapper;
using MediatR;
using OneOf;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Utils;
using Questao5.Utils.Enum;

namespace Questao5.Application.Handlers
{
    public class CreateMovimentHandler : IRequestHandler<CreateMovimentRequest, OneOf<Result<CreateMovimentResponse>, ErrorResult>>
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public CreateMovimentHandler(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OneOf<Result<CreateMovimentResponse>, ErrorResult>> Handle(CreateMovimentRequest command, CancellationToken cancellationToken)
        {
            if (command.Tipo is null || (command.Tipo != TipoMovimentoEnum.Debito.ToDescriptionString()
            && command.Tipo != TipoMovimentoEnum.Credito.ToDescriptionString()))
                return new ErrorResult(TiposRetornoEnum.TipoInvalido);

            if (command.Valor <= 0)
                return new ErrorResult(TiposRetornoEnum.ValorInvalido);

            var accountResponse = await _repository.GetAccountByIdAsync(command.IdContaCorrente);
            if (accountResponse == null)
                return new ErrorResult(TiposRetornoEnum.MovimentoContaInvalida);
            else if (accountResponse.Ativo != UserStatus.Ativo)
                return new ErrorResult(TiposRetornoEnum.MovimentoContaInativa);

            var result = await _repository.AddMovimentAsync(_mapper.Map<MovimentDataRequest>(command));
            if (result.IsT1)
                return result.AsT1;

            return new Result<CreateMovimentResponse>(new CreateMovimentResponse() { IdMovimentacao = result.AsT0.Response.Id.ToUpper() });
        }
    }
}
