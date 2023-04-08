namespace SirvaMe.Models
{
    /// <summary>
    /// Review of the Professional given by the Client
    /// </summary>
    public class Avaliacao
    {
        public int PrestadorId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteAvatar { get; set; }
        public int AgendamentoId { get; set; }
    }
}
