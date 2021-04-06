using Agencia_Pil_Movil.Models;
using Firebase.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil_Movil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalMaster : ContentPage
    {

        public PrincipalMaster()
        {
            InitializeComponent();


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Actualizar
            var pag = App.Current.MainPage as Principal;
            pag.Detail.Navigation.PopToRootAsync();
            pag.Detail.Navigation.PushAsync(new ActualizarPage());
                (pag as MasterDetailPage).IsPresented = false;


        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //cerrar Sesion
            using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Usuario>();
                conn.DeleteAll<Usuario>();

                FirebaseClient firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/" + App.usuario.ci_usuario);
                var productosin=firebase
                  .Child("Productos")
                  .OnceAsync<Producto>().Result.Select(item => new Producto
                  {
                      categoria = item.Object.categoria,
                      id_producto = item.Object.id_producto,
                      codigo = item.Object.codigo,
                      detalle = item.Object.detalle,
                      imagen = item.Object.imagen,
                      marca = item.Object.marca,
                      nombre = item.Object.nombre,
                      Presentacion = item.Object.Presentacion,
                      tipo = item.Object.tipo
                  }).ToList();
                if (productosin.Count>0)
                {
                    conn.InsertAll(productosin);
                }
                var Precios_mein=firebase
                  .Child("Precios_Menor")
                  .OnceAsync<Precio>().Result.Select(item => new Precio
                  {
                      precio = item.Object.precio,
                      id_producto = item.Object.id_producto,
                      fechaini = item.Object.fechaini,
                      estado = item.Object.estado
                  }).ToList();
                if (Precios_mein.Count>0)
                {
                    conn.InsertAll(Precios_mein);
                }
                var Precios_main = firebase
                  .Child("Precios_Mayor")
                  .OnceAsync<Precio>().Result.Select(item => new Precio
                  {
                      precio = item.Object.precio,
                      id_producto = item.Object.id_producto,
                      fechaini = item.Object.fechaini,
                      estado = item.Object.estado
                  }).ToList();
                if (Precios_main.Count>0)
                {
                    conn.InsertAll(Precios_main);

                }
                var precios_Ma = conn.Table<Precio_Mayor>().ToList();
                var precios_Me = conn.Table<Precio>().ToList();
                var productos = conn.Table<Producto>().ToList();

                for (int i = 0; i < productos.Count; i++)
                {
                    await firebase
                        .Child("Productos")
                        .PostAsync(productos[i]);
                }
                for (int i = 0; i < precios_Ma.Count; i++)
                {
                    await firebase
                        .Child("Precios_Mayor")
                        .PostAsync(precios_Ma[i]);
                }
                for (int i = 0; i < precios_Me.Count; i++)
                {
                    await firebase
                        .Child("Precios_Menor")
                        .PostAsync(precios_Me[i]);
                }
                conn.DeleteAll<Precio_Mayor>();
                conn.DeleteAll<Precio>();
                conn.DeleteAll<Producto>();
            }
            App.Current.MainPage =new loginPage();
        }
    }
}