using System.ComponentModel.DataAnnotations.Schema;

namespace prototipo1204.Models
{
    public class Produto
    {
        public int idProd { get; set; }
        public string codBar { get; set; }
        public string nomeProd { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal precoProd { get; set; }

        public uint qtdProd { get; set; }
        public string marcaProd { get; set; }
        public string descricaoProd { get; set; }


        public int idCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public List<ItemPedido> ItemPedido { get; set; }
    }
}
