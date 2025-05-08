using System.ComponentModel.DataAnnotations.Schema;

namespace prototipo1204.Models
{
    public class Pedido
    {
        public int idPed { get; set; }
        public int? idCliente { get; set; }
        public int idEnd { get; set; }
        public int? idPag { get; set; }
        public DateTime dataPed { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal totalPed { get; set; }
        public Endereco? Endereco { get; set; } // fk
        public Cliente? Cliente { get; set; } // fk 
        public Pagamento? Pagamento { get; set; } //fk 
        public List<ItemPedido> ItemPedido { get; set; }

    }
}
