using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil_Movil.Models
{
    public class MarcaView
    {
        public string  nombre { get; set; }
        private string Imagen; // field
        public string imagen   // property
        {
            get { return Imagen; }
            set { Imagen = value.Replace(" ","_");
                if (value.Equals("REFRESCA-T"))
                {
                    Imagen="REFRESCA.jpg";
                }
            }
        }
        public int cantidad { get; set; }

        
    }
}
