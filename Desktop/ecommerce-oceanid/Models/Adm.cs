namespace prototipo1204.Models
{
    public class Adm
    {
        public int idAdm { get; set; }
        public string nomeAdm { get; set; }
        public string senhaAdm { get; set; }
        public string emailAdm { get; set; }
        public List<Login> Login { get; set; }

    }
}
