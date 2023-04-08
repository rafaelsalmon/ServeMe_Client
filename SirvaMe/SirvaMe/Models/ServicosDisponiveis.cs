using System;

namespace SirvaMe.Models
{
    /// <summary>
    /// Available services
    /// </summary>
    public class ServicosDisponiveis
    {
        public int Id { get; set; }
        public int PrestadorId { get; set; }
        public string NomeServico { get; set; }
        public string TipoPrestador { get; set; }
        public DateTime DataServico { get; set; }
        public bool ServicoConfirmadoPrestador { get; set; }
        public bool ServicoRecusadoPrestador { get; set; }
        public bool ServicoConfirmadoCliente { get; set; }
        public bool ServicoRecusadoCliente { get; set; }
    }
}
