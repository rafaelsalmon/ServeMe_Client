using System;
using SQLite.Net.Attributes;

namespace SirvaMe.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class Usuario
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Foto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string FacebookToken { get; set; }

        public string EmailCliente { get; set; }
        public string PushTokenCliente { get; set; }
        public int PushPlataformaCliente { get; set; }
        public string TelefoneCliente { get; set; }

        public string EmailPrestador { get; set; }
        public string PushTokenPrestador { get; set; }
        public int PushPlataformaPrestador { get; set; }
        public string TelefonePrestador { get; set; }
        public int Nota { get; set; }
        public string Apresentacao { get; set; }
    }
}
