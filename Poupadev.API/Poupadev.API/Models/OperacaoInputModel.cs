using Poupadev.API.Enums;

namespace Poupadev.API.Models
{
    public class OperacaoInputModel
    {
        public decimal Valor { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
    }
}
