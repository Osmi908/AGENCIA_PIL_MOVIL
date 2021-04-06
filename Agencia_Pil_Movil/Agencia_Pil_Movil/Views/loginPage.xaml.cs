using Agencia_Pil_Movil.Models;
using Firebase.Database;
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
    public partial class loginPage : ContentPage
    {
        public Usuario usuari { get; set; }
        FirebaseClient firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/");
        public loginPage()
        {
            
            InitializeComponent();
        }
        public List<Usuario> Obtener_Usuarios()
        {
            return (firebase
              .Child("Usuarios")
              .OnceAsync<Usuario>()).Result.Select(item => new Usuario
              {
                  apMaterno = item.Object.apMaterno,
                  apPaterno = item.Object.apPaterno,
                  ci_usuario = item.Object.ci_usuario,
                  estado = item.Object.estado,
                  direccion= item.Object.direccion,
                  fecha_inicio=item.Object.fecha_inicio,
                  nombre=item.Object.nombre,
                  password=item.Object.password,
                  rol=item.Object.rol,
                  telefono=item.Object.telefono,
                  usuario=item.Object.usuario,
                  
              }).ToList();
        }
        private async void btningresar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                int dat = 0;
                string usuario = txtusuario.Text;
                string contraseña = txtcontraseña.Text;
                var usuarios = Obtener_Usuarios();
                bool sw = false;
                for (int i = 0; i < usuarios.Count; i++)
                {
                    if (usuarios[i].usuario.Equals(usuario) && usuarios[i].password.Equals(contraseña))
                    {
                        usuari = usuarios[i];
                        dat = i;
                        sw = true;
                        break;
                    }

                }
                if (sw)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
                    {
                        conn.DeleteAll<Usuario>();
                        conn.CreateTable<Usuario>();
                        conn.Insert(usuari);
                    }
                    string nombreCompleto = "Hola!! " + usuari.nombre + " " + usuari.apPaterno + " " + usuari.apMaterno + " eres " + usuari.rol + "'";
                    await DisplayAlert("BIENVENIDO !!", nombreCompleto, "OK");
                    App.usuario = usuari;
                    App.Current.MainPage = new Principal(usuari);

                }
                else
                {
                    await DisplayAlert("Datos incorrectos", "intenta nuevamente", "OK");
                }




            }
            
        }

        private bool validarDatos()
        {
            bool sw = false;
            if (txtusuario.Text.Equals("") && txtcontraseña.Text.Equals(""))
            {
                txtcontraseña.BackgroundColor = Color.DarkRed;
                txtusuario.BackgroundColor = Color.DarkRed;
                
            }
            else
            {
                if (txtcontraseña.Text.Equals(""))
                {
                    txtcontraseña.BackgroundColor = Color.DarkRed;
                }
                else
                {
                    if (txtusuario.Text.Equals(""))
                    {
                        txtusuario.BackgroundColor = Color.DarkRed;
                    }
                    else
                    {
                        sw = true;
                    }
                    
                }
                
                
            }
            return sw;
        }

        private void txtusuario_Focused(object sender, FocusEventArgs e)
        {
            txtcontraseña.BackgroundColor = Color.Transparent;
            txtusuario.BackgroundColor = Color.Transparent;
        }
    }
}