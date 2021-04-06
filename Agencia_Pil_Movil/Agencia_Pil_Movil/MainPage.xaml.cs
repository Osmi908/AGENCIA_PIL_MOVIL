using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.ViewModels;
using Agencia_Pil_Movil.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agencia_Pil_Movil
{
    public partial class MainPage : ContentPage
    {
        public MarcasViewModel marcasVM { get; set; }
        public MainPage()
        {
            InitializeComponent();
            marcasVM = new MarcasViewModel();
            marcasVM.productos = App.carritoVenta;
            BindingContext= marcasVM;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var marca=(MarcaView)e.Item;
            Navigation.PushAsync(new categoriaPage(marca));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new VenderPage());
        }
        protected override void OnAppearing()
        {
            marcasVM.ActualizarProductosVenta();
            lblcarrito.Text = marcasVM.productosVenta;
        }
    }
}
