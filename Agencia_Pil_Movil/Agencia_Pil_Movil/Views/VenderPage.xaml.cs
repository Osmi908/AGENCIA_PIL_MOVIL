using Agencia_Pil.Models;
using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.ViewModels;
using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil_Movil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VenderPage : ContentPage
    {
        public List<Detalle_Venta> venta_detalle { get; set; }
        FirebaseClient firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/" + App.usuario.ci_usuario);
        public int CantidadTotal { get; set; }
        public decimal PrecioTotal { get; set; }
        public List<Producto_Precio> Productos_Venta { get; set; }
        public VenderPage()
        {
            venta_detalle = new List<Detalle_Venta>();
            Productos_Venta = App.carritoVenta;
            InitializeComponent();
            BindingContext = new VentaViewModel();
            lstProductosVenta.ItemsSource = Productos_Venta;
            LlenarDatos();
            txtDatCant.Text = CantidadTotal.ToString();
            txtDatPrecio.Text =PrecioTotal.ToString();
        }

        private void LlenarDatos()
        {

            for (int i = 0; i < Productos_Venta.Count; i++)
            {
                PrecioTotal += Productos_Venta[i].precio_cantidad;
                CantidadTotal += Productos_Venta[i].cantidad;
                venta_detalle.Add(new Detalle_Venta() { cantidad = Productos_Venta[i].cantidad, codigo_producto = Productos_Venta[i].cod_producto, precio_unitario = Productos_Venta[i].precio_unitario, precio_total = Productos_Venta[i].precio_cantidad });
            }
        }
        private async void btnVenta_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.DeleteAll<Venta>();
                conn.CreateTable<Detalle_Venta>();
                conn.CreateTable<Venta>();
                Venta venta = new Venta() { cantidad_productos=CantidadTotal,Monto_total=PrecioTotal, ci_usuario=App.usuario.ci_usuario,estado="Finalizado",fecha=DateTime.Now,id_cliente=int.Parse(txtnit.Text)};
                conn.Insert(venta);
                 var eñem=conn.Table<Venta>().ToList();
                var may=conn.Query<Usuario>("SELECT MAX(id_venta) as nombre, MAX(id_venta) as ci_usuario From Venta");
                for (int i = 0; i < venta_detalle.Count; i++)
                {
                    venta_detalle[i].id_venta = may[0].ci_usuario;
                }
                conn.InsertAll(venta_detalle);

                EnviarVenta(venta);

                await DisplayAlert("Listo ","Se registro la venta con exito","Ok");
                App.carritoVenta = new List<Producto_Precio>();
               await Navigation.PopToRootAsync();
               await Navigation.PushAsync(new VenderPage());
            }
        }
        public async Task EnviarVenta(Venta venta)
        {

            await firebase
              .Child("Ventas")
              .PostAsync(venta);
            for (int i = 0; i < venta_detalle.Count; i++)
            {
                await firebase
                    .Child("Detalle_Ventas")
                    .PostAsync(venta_detalle[i]);
            }

        }
    }
}