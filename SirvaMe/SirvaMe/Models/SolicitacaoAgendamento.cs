using System;

namespace SirvaMe.Models
{
    /// <summary>
    /// Schedule request
    /// </summary>
    public class SolicitacaoAgendamento
    {
        public int ServicoId { get; set; }
        public string Prestador { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }
    }
}
