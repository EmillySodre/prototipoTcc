namespace prototipo1204.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nomeCategoria { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
