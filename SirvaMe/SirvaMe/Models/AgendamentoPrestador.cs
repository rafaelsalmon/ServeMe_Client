namespace SirvaMe.Models
{
    /// <summary>
    /// Acceptance status of the appointment by the Professional
    /// </summary>
    public class AgendamentoPrestador
    {
        public int Id { get; set; }
        public int PrestadorId { get; set; }
        public int AgendamentoId { get; set; }
        public int AgendamentoStatus { get; set; }

        public enum AceiteRecusa { Aceitou = 1, Recusou = 2 }
    }
}
