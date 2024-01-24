using System.ComponentModel;
using System.Runtime.Serialization;

namespace Questao5.Utils.Enum
{
    public enum TiposRetornoEnum
    {
        [Description("INVALID_ACCOUNT")]
        MovimentoContaInvalida= 1,
        [Description("INACTIVE_ACCOUNT")]
        MovimentoContaInativa= 2,
        [Description("INVALID_VALUE")]
        ValorInvalido = 3,
        [Description("INVALID_TYPE")]
        TipoInvalido = 4,
        [Description("INVALID_ACCOUNT")]
        ConsultaContaInvalida = 5,
        [Description("INACTIVE_ACCOUNT")]
        ConsultaContaInativa = 6,
        [Description("ERROR_INSERT")]
        InsertError = 7,
        [Description("ERROR_GENERICO")]
        ErroGenerico = 8
    }
}
