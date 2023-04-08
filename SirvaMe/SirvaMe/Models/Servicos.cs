namespace SirvaMe.Models
{
    /// <summary>
    /// Services
    /// </summary>
    public class Servicos
    {
        public int Id { get; set; }
        public int PrestadorId { get; set; }
        public string Nome { get; set; }
        public string LinkFoto { get; set; }
        public string Descricao { get; set; }
        public bool Checado { get; set; }
    }
}
