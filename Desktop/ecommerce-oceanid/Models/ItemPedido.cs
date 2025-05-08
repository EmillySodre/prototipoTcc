namespace prototipo1204.Models
{
    public class ItemPedido
    {
        public int idProdutoPedido { get; set; }
        public int idPedido { get; set; }
        public int idProd { get; set; }
        public int quantidade { get; set; }
        public decimal precoUnitario { get; set; }

        // FKs
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
       
    }
}
