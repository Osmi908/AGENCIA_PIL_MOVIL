using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.ViewModels;
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
    public partial class categoriaPage : ContentPage
    {
        public CategoriaViewModel categoriaVM { get; set; }
        public categoriaPage(MarcaView marca)
        {
            categoriaVM = new CategoriaViewModel(marca);
            InitializeComponent();
            BindingContext = categoriaVM;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var categoria = (CategoriaView)e.Item;
            Navigation.PushAsync(new ProductoPage(categoria));

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VenderPage());
        }
    }
}