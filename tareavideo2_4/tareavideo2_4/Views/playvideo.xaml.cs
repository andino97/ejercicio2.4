using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tareavideo2_4.Models;
using Xam.Forms.VideoPlayer;


namespace tareavideo2_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class playvideo : ContentPage
    {
        public playvideo()
        {
            InitializeComponent();

            descripcion.Text = datos.VideoDescripcion;
            UriVideoSource uriVideoSurce = new UriVideoSource()
            {
                Uri = datos.VideoUri
            };
            videoPlayer.Source = uriVideoSurce;
        }

        private void videoPlayer_PlayCompletion(object sender, EventArgs e)
        {

        }

        private async void btnExitApp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}