namespace prototipo1204.Models
{ 
    public class Cliente
    {
        public int idCliente { get; set; }
        public string cpf { get; set; }
        public string nomeCompleto { get; set; }
        public DateOnly dataNasc { get; set; }
        public string senhaCliente { get; set; }
        public string emailCliente { get; set; }
        public int? idEnd {  get; set; }
        public Endereco? endereco {  get; set; }
        public List<Pedido> Pedido { get; set; }
        public List<Login> Login { get; set; }
    }
}
