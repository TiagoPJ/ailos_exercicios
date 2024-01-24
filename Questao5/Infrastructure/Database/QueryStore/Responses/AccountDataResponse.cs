using FluentAssertions.Equivalency;
using Questao5.Domain.Entities;
using Questao5.Utils.Enum;
using System.ComponentModel;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class AccountDataResponse
    {
        public AccountDataResponse()
        {
            Movimentos = new List<Moviment>();
        }
        [Description("IdContaCorrente")]
        public string Id { get; set; }

        [Description("Numero")]
        public int Conta { get; set; }

        [Description("Nome")]
        public string Titular { get; set; }
        public UserStatus Ativo { get; set; }
        public decimal ValorTotalDebito => Movimentos.Where(x => x.Tipo.Equals(TipoMovimentoEnum.Debito.ToDescriptionString(), StringComparison.Ordinal)).Sum(y => y.Valor);
        public decimal ValorTotalCredito => Movimentos.Where(x => x.Tipo.Equals(TipoMovimentoEnum.Credito.ToDescriptionString(), StringComparison.Ordinal)).Sum(y => y.Valor);
        public decimal Saldo => this.ValorTotalCredito - this.ValorTotalDebito;
        public List<Moviment> Movimentos { get; set; }

    }
}
