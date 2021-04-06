using Agencia_Pil_Movil.Models;
using Firebase.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia_Pil_Movil.ViewModels
{
    public class ActualizarViewModel
    {
        public Usuario usuario { get; set; }
        public List<Precio> Precios { get; set; }
        public List<Producto> ProductosNuevos { get; set; }
        public List<Precio_Mayor> PreciosMayor { get; set; }
        public List<Producto> ProductosSinStock { get; set; }
        FirebaseClient firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/" + App.usuario.ci_usuario);
        public ActualizarViewModel()
        {
            Precios = ObtenerPrecios();
            PreciosMayor = ObtenerPrecios_Mayor();
            ProductosNuevos = ObtenerProductosNuevos();

        }
        public List<Precio> ObtenerPrecios()
        {

            return (firebase
              .Child("Precios_Menor")
              .OnceAsync<Precio>()).Result.Select(item => new Precio
              {
                  precio = item.Object.precio,
                  id_producto = item.Object.id_producto,
                  fechaini = item.Object.fechaini,
                  estado = item.Object.estado
              }).ToList();
        }

        internal async Task<bool> ActualizarTodo()
        {
            using (SQLiteConnection conn= new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.InsertAll(ProductosNuevos);
                conn.InsertAll(Precios);
                conn.InsertAll(PreciosMayor);

            }; 
            try
            {
               await firebase
                    .Child("Precios_Menor").DeleteAsync();
                await firebase
                    .Child("Precios_Mayor").DeleteAsync();
                await firebase
                    .Child("Productos").DeleteAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        //public async Task AddVehicle(string myKey, string myImageUri)
        //{

        //    await firebase
        //      .Child("Vehicle")
        //      .PostAsync(new Precio());
        //}


        private List<Precio_Mayor> ObtenerPrecios_Mayor()
        {
            return (firebase
                .Child("Precios_Mayor")
                .OnceAsync<Precio_Mayor>()).Result.Select(item => new Precio_Mayor
                {
                    precio = item.Object.precio,
                    id_producto = item.Object.id_producto,
                    fechaini = item.Object.fechaini,
                    estado = item.Object.estado
                }).ToList();
        }

        public List<Producto> ObtenerProductosNuevos()
        {

            return (firebase
              .Child("Productos")
              .OnceAsync<Producto>()).Result.Select(item => new Producto
              {
                  categoria = item.Object.categoria,
                  id_producto = item.Object.id_producto,
                  codigo = item.Object.codigo,
                  detalle = item.Object.detalle,
                  imagen=item.Object.imagen,
                  marca= item.Object.marca,
                  nombre= item.Object.nombre,
                  Presentacion= item.Object.Presentacion,
                  tipo= item.Object.tipo
              }).ToList();
        }


    }
    
}
