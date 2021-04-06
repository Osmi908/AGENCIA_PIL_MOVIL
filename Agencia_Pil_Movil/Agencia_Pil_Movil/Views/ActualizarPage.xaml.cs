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
    public partial class ActualizarPage : ContentPage
    {
        public ActualizarViewModel actVM { get; set; }
        public ActualizarPage()
        {
            actVM = new ActualizarViewModel();
            InitializeComponent();
            BindingContext = actVM;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (await actVM.ActualizarTodo())
            {
                await DisplayAlert("Listo"," Actualizaste los Datos", "OK");
            }
            else
            {
                await DisplayAlert("Error", " Algo salio mal intente nuevamente", "OK");
            }
            ;
        }
    }
}