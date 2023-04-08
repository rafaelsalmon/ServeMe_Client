namespace SirvaMe.Models
{
    /// <summary>
    /// Address
    /// </summary>
    public class Endereco
    {
        public int Id { get; set; }
        public string TipoDeLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string PontoReferencia { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string RetornaEnderecoCompleto()
        {
            return  $"{Logradouro}, {Numero} {Complemento} - {Bairro} - {Cidade} - {Estado}";
        }

        public string RetornaEnderecoCompleto(Endereco end)
        {
            return $"{end.TipoDeLogradouro} {end.Logradouro}, {end.Numero} {end.Complemento} - {end.Bairro} - {end.Cidade} - {end.Estado}";
        }
    }
}
