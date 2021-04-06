using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class DetallePedido
    {
        public int id_pedido { get; set; }
        public string codigo_producto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double precio_total { get; set; }
    }
}
