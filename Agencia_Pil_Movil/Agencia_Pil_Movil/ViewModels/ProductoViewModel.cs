using Agencia_Pil_Movil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Agencia_Pil_Movil.ViewModels
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        string dateTime;
        public List<Producto> productos { get; set; }
        public List<Precio> PreciosProducto { get; set; }
        public List<Precio_Mayor> PreciosProductoMayor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ProductoViewModel(CategoriaView cat)
        {
            dateTime = "";
            productosVenta = "hay " + App.carritoVenta.Count + " Productos en el carrito";
            using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                string query = "SELECT * FROM Producto Where categoria like '" + cat.nombre + "' and marca like '" + cat.marca + "'";
                productos = conn.Query<Producto>(query);

            }
        }
        public string productosVenta
        {
            set
            {
                if (!dateTime.Equals(value))
                {
                    dateTime = value;

                    OnPropertyChanged("productosVenta");
      
                }
            }
            get
            {
                return dateTime;
            }
        }

        private void SetcambiarLista()
        {
            throw new NotImplementedException();
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        //public string ProductosVenta { get; set; }
        //public string productosVenta
        //{
        //    set
        //    {
        //        if (productosVenta != value)
        //        {
        //            productosVenta = value;

        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged(this, new PropertyChangedEventArgs("productosVenta"));
        //            }
        //        }
        //    }
        //    get
        //    {
        //        return productosVenta;
        //    }
        //}
        //public List<Producto> productos { get; set; }
        //public ProductoViewModel(CategoriaView cat)
        //{
        //    productosVenta = "hay " + App.carritoVenta.Count + " Productos en el carrito";
        //    using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
        //    {
        //        string query = "SELECT * FROM Producto Where categoria like '" + cat.nombre + "' and marca like '" + cat.marca + "'";
        //        productos = conn.Query<Producto>(query);

        //    }
        //}


    }
}
