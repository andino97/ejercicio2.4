using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tareavideo2_4.Views;
using tareavideo2_4.Controller;
using System.IO;

namespace tareavideo2_4
{
    public partial class App : Application
    {
        static VideoDBController BaseDatos;

        public static VideoDBController BaseDatosObject
        {
            get
            {
                if (BaseDatos == null)
                {
                    BaseDatos = new VideoDBController(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VideosDBApp.db3"));
                }
                return BaseDatos;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new grabarvideo());
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
