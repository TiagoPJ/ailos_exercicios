using AutoMapper;
using Castle.Core.Resource;
using MediatR;
using OneOf;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers.Interfaces;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Utils;
using Questao5.Utils.Enum;

namespace Questao5.Application.Handlers
{
    public class FindAccountByIdHandler : IRequestHandler<FindAccountByIdRequest, OneOf<Result<FindAccountByIdResponse>, ErrorResult>>
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public FindAccountByIdHandler(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OneOf<Result<FindAccountByIdResponse>, ErrorResult>> Handle(FindAccountByIdRequest command, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAccountByIdAsync(command.Id);
            if (result == null)
                return new ErrorResult(TiposRetornoEnum.ConsultaContaInvalida);
            else if (result.Ativo != UserStatus.Ativo)
                return new ErrorResult(TiposRetornoEnum.ConsultaContaInativa);

            return new Result<FindAccountByIdResponse>(_mapper.Map<FindAccountByIdResponse>(result));
        }
    }
}
