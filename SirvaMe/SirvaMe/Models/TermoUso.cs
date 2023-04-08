namespace SirvaMe.Models
{
    /// <summary>
    /// Terms of Service helper
    /// </summary>
    public class TermoUso
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Tipo { get; set; }

        public enum TipoTermo { Cliente = 1, Prestador = 2 }
    }
}
