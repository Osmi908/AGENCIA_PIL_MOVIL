using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.ViewModels;
using Rg.Plugins.Popup.Services;
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
    public partial class ProductoPage : ContentPage
    {
        public ProductoViewModel prodVM { get; set; }
        public CategoriaView categoria { get; set; }
        public ProductoPage(CategoriaView cat)
        {
            prodVM = new ProductoViewModel(cat);
            categoria = cat;
            InitializeComponent();
            BindingContext = prodVM;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var prod =(Producto) e.Item;
            await PopupNavigation.PushAsync(new AgregarCarritoPage(prod));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VenderPage());
        }

    }
}