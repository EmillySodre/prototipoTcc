namespace prototipo1204.Models
{
    public class Endereco
    {
        public int idEnd { get; set; }
        public string cepEnd { get; set; }
        public int numeroEnd { get; set; }
        public string logradouro { get; set; }
        public string? complemento { get; set; }
        public string? bairro { get; set; }
        public string? estado { get; set; }
        public string cidade { get; set; }
        public List<Pedido> Pedido { get; set; }
        public List<Cliente> clientes { get; set; }
    }
}
