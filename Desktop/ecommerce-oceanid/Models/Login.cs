namespace prototipo1204.Models
{
    public class Login
    {
        public int idLogin {  get; set; }
        public int? idCliente { get; set; }
        public Cliente cliente { get; set; }
        public int? idAdm { get; set; }
        public Adm adm { get; set; }
    }
}
