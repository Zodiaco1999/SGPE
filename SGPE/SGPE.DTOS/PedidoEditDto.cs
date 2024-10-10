using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPE.DTOS
{
    public class PedidoEditDto
    {
        public long IdPedido { get; set; }
        public long IdUsuarioSolicita { get; set; }
        public long IdEstadoPedido { get; set; }

        public decimal ValorTotal { get; set; }
        //public DateTime FechaRegistro { get; set; }
        public ICollection<DetallePedidoDto> DetallePedidos { get; set; } = null!;
    }
}
