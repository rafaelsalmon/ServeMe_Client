using System;

namespace SirvaMe.Models
{
    /// <summary>
    /// Appointment
    /// </summary>
    public class Agendamento
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PrestadorId { get; set; }
        public int TipoServicoId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public int EnderecoId { get; set; }
        public bool PrestadorSelecionado { get; set; }
        public bool ServicoConfirmadoPrestador { get; set; }
        public bool ServicoConfirmadoCliente { get; set; }
        public bool AgendamentoCanceladoPrestador { get; set; }
        public bool AgendamentoCanceladoCliente { get; set; }
        public bool Agendado { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
