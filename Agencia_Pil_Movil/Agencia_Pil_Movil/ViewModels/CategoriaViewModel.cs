using Agencia_Pil_Movil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil_Movil.ViewModels
{
    public class CategoriaViewModel
    {
        public string productosVenta { get; set; }
        public List<CategoriaView> categorias { get; set; }
        public CategoriaViewModel(MarcaView marca)
        {
            productosVenta = "hay " + App.carritoVenta.Count + " Productos en el carrito";
            categorias = new List<CategoriaView>();
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                string Query = "SELECT p.categoria as nombre, COUNT(p.id_producto)cantidad, p.categoria||'.jpg' as imagen, p.marca FROM(SELECT * FROM Producto where marca like '"+marca.nombre+"') p GROUP BY(p.categoria)";
                categorias=conn.Query<CategoriaView>(Query);
            }

        }
    }
}
