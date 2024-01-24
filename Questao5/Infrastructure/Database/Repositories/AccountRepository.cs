using AutoMapper;
using Castle.Core.Resource;
using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using OneOf;
using OneOf.Types;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Extensao;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Questao5.Utils;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SqliteConnection _sqliteConnection;
        private readonly IMapper _mapper;

        public AccountRepository(DatabaseConfig dataBase, IMapper mapper)
        {
            _sqliteConnection = new SqliteConnection(dataBase.Name);
            _mapper = mapper;
        }

        public void Iniciar()
        {
            DapperMapper.Mapper<AccountDataResponse>();
            DapperMapper.Mapper<MovimentDataResponse>();
            DapperMapper.Mapper<Moviment>();
        }

        public async Task<AccountDataResponse> GetAccountByIdAsync(string id)
        {
            var sql = @"SELECT c.*  " +
                       "FROM contacorrente c " +
                       "WHERE c.idcontacorrente = @id;";

            var account = await _sqliteConnection.QueryFirstOrDefaultAsync<AccountDataResponse>(sql, new { id });
            if (account != null)
            {
                var movimentacoes = await this.GetMovimentsByIdAccount(id);
                if (movimentacoes != null && movimentacoes.Any())
                    account.Movimentos.AddRange(_mapper.Map<IEnumerable<Moviment>>(movimentacoes));
            }

            return account;
        }

        public async Task<IEnumerable<MovimentDataResponse>> GetMovimentsByIdAccount(string id)
        {
            var sql = @"SELECT m.*  " +
                        "FROM movimento m " +
                        "WHERE m.idcontacorrente = @id;";


            return await _sqliteConnection.QueryAsync<MovimentDataResponse>(sql, new { id });
        }

        public async Task<OneOf<Utils.Result<MovimentDataResponse>, ErrorResult>> AddMovimentAsync(MovimentDataRequest command)
        {
            try
            {
                var pk_idMovimento = await _sqliteConnection.ExecuteScalarAsync<int>("INSERT INTO movimento (IdMovimento, IdContaCorrente, DataMovimento, TipoMovimento, Valor) " +
                "VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor); select last_insert_rowid();", new { command.IdMovimento, command.IdContaCorrente, command.DataMovimento, command.TipoMovimento, command.Valor });

                if (!string.IsNullOrEmpty(command.IdMovimento))
                    return new Utils.Result<MovimentDataResponse>(new MovimentDataResponse { Id = command.IdMovimento });
                else
                    return new ErrorResult(Utils.Enum.TiposRetornoEnum.InsertError);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Utils.Enum.TiposRetornoEnum.InsertError, ex.Message);
            }
        }
    }
}
