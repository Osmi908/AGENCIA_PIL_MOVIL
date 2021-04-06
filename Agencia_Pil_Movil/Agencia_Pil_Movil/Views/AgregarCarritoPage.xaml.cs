using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class AgregarCarritoPage : PopupPage
    {
        public string tipoPrecio { get; set; }
        public AgregarCarritoViewModel agreVM { get; set; }
        public Producto Producto { get; set; }
        public AgregarCarritoPage(Producto producto)
        {
            
            this.Producto = producto;
            agreVM = new AgregarCarritoViewModel(this.Producto);
            InitializeComponent();
            BindingContext = agreVM;
            if (agreVM.productoCPrescios.Precio_Mayor==0)
            {
                btn_precioMayor.IsVisible = false;
            }
            if (agreVM.productoCPrescios.Precio_Menor==0)
            {
                btn_precioMenor.IsVisible = false;
            }
            if (agreVM.productoCPrescios.Precio_Menor == 0 && agreVM.productoCPrescios.Precio_Mayor == 0)
            {
                txtsinprecio.IsVisible = true;

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {//menor

            //decimal precio = 0;
            //string cantidadCad = 
            //int cantidad = int.Parse(cantidadCad);
            //using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            //{
            //    conn.CreateTable<Producto_Precio>();
            //    string query = "SELECT* FROM Precio WHERE estado = 1 and id_producto like '" + Producto.codigo + "'";
            //    var precios = conn.Query<Precio_Mayor>(query);
            //    if (precios.Count > 0)
            //    {
            //        precio = precios[0].precio;
            //    }

            //    Producto_Precio productoPrecio = new Producto_Precio() { cantidad = cantidad, imagen_producto = Producto.imagen, cod_producto = Producto.codigo, nombre_producto = Producto.nombre, precio_unitario = precio, precio_cantidad = cantidad * precio };
            //    App.carritoVenta.Add(productoPrecio);

            //}


            //await DisplayAlert("Precio por Menor", "Listo añadiste un producto al carrito", "CONTINUAR");
            //await PopupNavigation.PopAllAsync();
            btn_precioMenor.IsVisible = false;
            frmeCantidad.IsVisible = true;
            tipoPrecio = "MAYOR";
            btn_precioMayor.IsEnabled = false; ;

        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            
            frmeCantidad.IsVisible = true;
            btn_precioMayor.IsVisible = false;
            btn_precioMenor.IsEnabled = false;
            tipoPrecio = "MENOR";
        }

        private async void btAgregarCarrito_Clicked(object sender, EventArgs e)
        {
            decimal precio = 0;
            string cantidadCad = txtCantidad.Text;
            int cantidad = int.Parse(cantidadCad);
                if (tipoPrecio.Equals("MENOR"))
                {
                    precio =agreVM.productoCPrescios.Precio_Menor;
                }
                else
                {
                    precio = agreVM.productoCPrescios.Precio_Mayor;
                }
                Producto_Precio productoPrecio = new Producto_Precio() { cantidad = cantidad, imagen_producto = Producto.imagen, cod_producto = Producto.codigo, nombre_producto = Producto.nombre, precio_unitario = precio, precio_cantidad = cantidad * precio };
            App.carritoVenta.Add(productoPrecio);


            await DisplayAlert("Listo", "AÑADISTE "+productoPrecio.nombre_producto+ " AL CARRITO", "CONTINUAR");
            await PopupNavigation.PopAllAsync();
            var pag = App.Current.MainPage as MasterDetailPage;
            int n = pag.Detail.Navigation.NavigationStack.Count - 1;
            var en = pag.Detail.Navigation.NavigationStack[n] as ProductoPage;
            en.prodVM.productosVenta = "hay " + App.carritoVenta.Count + " Productos en el carrito";
        }
    }
}