using Agencia_Pil.Models;
using Agencia_Pil_Movil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Agencia_Pil_Movil.ViewModels
{
    public class MarcasViewModel :INotifyPropertyChanged
    {
        private int nroVenta;
        public int NroVenta {
            set
            {
                if (nroVenta != value)
                {
                    nroVenta = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("NroVenta"));
                    }
                }
            }
            get
            {
                return nroVenta;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        DateTime dateTime;
        public List<Producto_Precio> productos { get; set; }
        public List<MarcaView> Marcas { get; set; }
        public string productosVenta { get; set; }
        public DateTime DateTime
        {
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("DateTime"));
                    }
                }
            }
            get
            {
                return dateTime;
            }
        }
        public MarcasViewModel()
        {
            this.DateTime = DateTime.Now;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.DateTime = DateTime.Now;
                return true;
            });
            productos = new List<Producto_Precio>();
            productosVenta = "hay "+productos.Count+" Productos en el carrito";
            Marcas = new List<MarcaView>();
            using (SQLiteConnection con=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                con.CreateTable<Venta>();
                con.CreateTable<Producto>();
                var ventasl = con.Table<Venta>().ToList();
                NroVenta = ventasl.Count + 1;
            }
            //Marcas.Add(new MarcaView() {
            //nombre="PIL",
            //cantidad=10,
            //imagen="PIL.png"});
            //Marcas.Add(new MarcaView()
            //{
            //    nombre = "COCA COLA",
            //    cantidad = 10,
            //    imagen = "COCA_COLA.png"
            //});
            //Marcas.Add(new MarcaView()
            //{
            //    nombre = "BONLE",
            //    cantidad = 10,
            //    imagen = "BONLE.png"
            //});
            //Marcas.Add(new MarcaView()
            //{
            //    nombre = "PURA VIDA",
            //    cantidad = 10,
            //    imagen = "PURA_VIDA.png"
            //});
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                string Query = "SELECT p.marca as nombre, COUNT(id_producto)cantidad, p.marca||'.png' as imagen from producto p Group by p.marca";
                
                Marcas= conn.Query<MarcaView>(Query);
            }
        }



        internal void ActualizarProductosVenta()
        {
            productosVenta = "hay " + productos.Count + " Productos en el carrito";
        }
    }
}
