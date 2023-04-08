namespace SirvaMe.Models
{
    /// <summary>
    /// Quotes (Proposals) by Professionals
    /// </summary>
    public class Propostas
    {
        public int AgendamentoId { get; set; }
        public Usuario Prestador { get; set; }
        public string Avatar { get; set; }
        public int TipoServicoId { get; set; }
        public string TipoServico { get; set; }
    }
}
