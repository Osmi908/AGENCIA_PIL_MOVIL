using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil_Movil.ViewModels
{
    public class VentaViewModel
    {
        public int NroVenta { get; set; }
        public VentaViewModel()
        {
            NroVenta = App.NroVentas;
        }
    }
}
