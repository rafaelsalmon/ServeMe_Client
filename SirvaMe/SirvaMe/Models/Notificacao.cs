namespace SirvaMe.Models
{
    /// <summary>
    /// Notification message
    /// </summary>
    public class Notificacao
    {
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int ServicoId { set; get; }
    }
}
