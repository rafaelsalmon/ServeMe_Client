using SQLite.Net.Attributes;

namespace SirvaMe.Models

    /// <summary>
    /// System helper
    /// </summary>
    public class Sistema
    {
        [PrimaryKey]
        public int Id { get; set; }
        public bool Logged { get; set; }
        public bool ManyLogged { get; set; }
        public string PushToken { get; set; }
    }
}