using Agencia_Pil_Movil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil_Movil.ViewModels
{
    public class AgregarCarritoViewModel
    {
        public Producto producto { get; set; }
        public ProductoPrecios productoCPrescios { get; set; }
        public List<ProductoPrecios> precios_MM { get; set; }
        public AgregarCarritoViewModel(Producto pro)
        {
            productoCPrescios = new ProductoPrecios();
            precios_MM = new List<ProductoPrecios>();
            producto = new Producto();
            producto = pro;
            BuscarPrecios();
            
        }

        private void BuscarPrecios()
        {
            using (SQLiteConnection conn= new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Precio>();

                conn.CreateTable<Precio_Mayor>();
                productoCPrescios.nombre_producto = producto.nombre;
                var mayor=conn.Query<ProductoPrecios>("SELECT precio as Precio_Mayor FROM Precio_Mayor Where id_producto like'" + producto.codigo+"'" );
                if (mayor.Count>0)
                {
                    productoCPrescios.Precio_Mayor = mayor[0].Precio_Mayor;
                }
                var menor = conn.Query<ProductoPrecios>("SELECT precio as Precio_Menor FROM Precio Where id_producto like'" + producto.codigo + "'");
                if (menor.Count>0)
                {
                    productoCPrescios.Precio_Menor = menor[0].Precio_Menor;
                }
                
                
            }
        }
    }
}
