using Questao5.Utils.Enum;

namespace Questao5.Utils
{
    public class TiposRetorno
    {
        public string CodigoDeErro { get; private init; }
        public string Mensagem { get; private init; } = null!;

        public static readonly Dictionary<TiposRetornoEnum, TiposRetorno> erros = new Dictionary<TiposRetornoEnum, TiposRetorno>()
    {
        { TiposRetornoEnum.MovimentoContaInvalida, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.MovimentoContaInvalida.ToDescriptionString(), Mensagem = "Apenas contas correntes cadastradas podem receber movimentação." } },
        { TiposRetornoEnum.MovimentoContaInativa, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.MovimentoContaInativa.ToDescriptionString(), Mensagem = "Apenas contas correntes ativas podem receber movimentação." } },
        { TiposRetornoEnum.ValorInvalido, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.ValorInvalido.ToDescriptionString(), Mensagem = "Apenas valores positivos ou maiores que zero podem ser recebidos." } },
        { TiposRetornoEnum.TipoInvalido, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.TipoInvalido.ToDescriptionString(), Mensagem = "Apenas os tipos “débito” ou “crédito” podem ser aceitos." } },
        { TiposRetornoEnum.ConsultaContaInvalida, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.ConsultaContaInvalida.ToDescriptionString(), Mensagem = "Apenas contas correntes cadastradas podem consultar o saldo." } },
        { TiposRetornoEnum.ConsultaContaInativa, new TiposRetorno() { CodigoDeErro = TiposRetornoEnum.ConsultaContaInativa.ToDescriptionString(), Mensagem = "Apenas contas correntes ativas podem consultar o saldo." } },
        { TiposRetornoEnum.InsertError, new TiposRetorno() { CodigoDeErro= TiposRetornoEnum.InsertError.ToDescriptionString(), Mensagem = "Erro ao tentar fazer o insert na tabela."} },
        { TiposRetornoEnum.ErroGenerico, new TiposRetorno() { CodigoDeErro= TiposRetornoEnum.ErroGenerico.ToDescriptionString(), Mensagem = "Ocorreu uma exceção - {0}."} }
    };
    }
}
