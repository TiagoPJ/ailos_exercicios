using Questao5.Utils.Enum;

namespace Questao5.Utils
{
    public abstract class Result
    {
        public bool Successo { get; set; }
    }

    public class ErrorResult : Result
    {
        public string Mensagem { get; set; }
        public string CodigoErro { get; set; }

        public ErrorResult(TiposRetornoEnum erro, string msgReplace = "")
        {
            Successo = false;
            Mensagem = string.Format(TiposRetorno.erros[erro].Mensagem, msgReplace);
            CodigoErro = TiposRetorno.erros[erro].CodigoDeErro;
        }
    }

    public class Result<T> : Result
    {
        public T Response { get; set; }

        public Result(T value)
        {
            Successo = true;
            Response = value;
        }
    }
}
