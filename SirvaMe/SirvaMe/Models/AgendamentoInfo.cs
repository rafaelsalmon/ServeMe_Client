using System;
using System.Collections.Generic;

namespace SirvaMe.Models
{
    /// <summary>
    /// Appointment Info
    /// </summary>
    public class AgendamentoInfo
    {
        public int Id { get; set; }
        public Usuario Cliente { get; set; }
        public string Avatar { get; set; }
        public int PrestadorId { get; set; }
        public int TipoServicoId { get; set; }
        public string TipoServico { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public string DataHoraTexto { get; set; }
        public Endereco Endereco { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }

        public List<AgendamentoPrestador> AgendamentoPrestador { get; set; }
        public List<PrestadorLocation> PrestadorLocation { get; set; }
    }
}
