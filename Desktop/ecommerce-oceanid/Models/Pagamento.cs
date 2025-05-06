using System.ComponentModel.DataAnnotations;

namespace prototipo1204.Models
{
    public class Pagamento
    {
        public int idPag { get; set; }

        [StringLength(50)]
        public string statusPag { get; set; } = "Pendente";  // Valor padrão vai ser Pendente

        [StringLength(50)]
        public string metodoPag { get; set; }

        public List<Pedido> Pedido { get; set; }
    }
}
