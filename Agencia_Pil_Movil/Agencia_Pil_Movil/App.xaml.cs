using Agencia_Pil.Models;
using Agencia_Pil_Movil.Models;
using Agencia_Pil_Movil.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil_Movil
{
    public partial class App : Application 
    {
        public static int NroVentas { get; set; }
        public static List<Producto_Precio> carritoVenta { get; set; }
        public static string ArchivoDBAgenciaPil = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AgenciPilMobil.db3");
        public static Usuario usuario { get; set; }

        public App()
        {
            carritoVenta = new List<Producto_Precio>();
            InitializeComponent();
            using (SQLiteConnection conn=new SQLiteConnection(ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Venta>();
                var ventas=conn.Table<Venta>().ToList();
                NroVentas = ventas.Count + 1;
                conn.CreateTable<Usuario>();

                List<Usuario> usuarios = new List<Usuario>();
                usuarios=conn.Table<Usuario>().ToList();
                if (usuarios.Count>0)
                {
                    usuario=usuarios[0];
                    MainPage=new Principal(usuarios[0]);
                }
                else
                {
                    var nav = new loginPage();
                    MainPage = nav;
                    usuario = new Usuario();
                }
            }


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
