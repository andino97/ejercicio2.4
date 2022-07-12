using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tareavideo2_4.Views;
using Xam.Forms.VideoPlayer;
using Xamarin.Essentials;
using System.IO;
using Plugin.Media;
using tareavideo2_4.Models;
using SQLite;

namespace tareavideo2_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class grabarvideo : ContentPage
    {
        public string PhotoPath;
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM02VideoApp.db3");
        string rutaDeVideo;
        public grabarvideo()
        {
            InitializeComponent();
        }

        private void btnRecordVideo_Clicked(object sender, EventArgs e)
        {
            RecorderVideoRealTime();
        }

        private void btnSaveVideo_Clicked(object sender, EventArgs e)
        {
            SaveVideoRecord();
        }

        private async void btnVideoList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new videolista());
        }


        private void videoPlayer_PlayCompletion(object sender, EventArgs e)
        {
        }


        private void btnExitApp_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }



        public async void RecorderVideoRealTime()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No hay camara", "No se detecta la camara.", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                {
                    Name = "video.mp4",
                    Directory = "DefaultVideos",
                });

                if (file == null)
                    return;

                await DisplayAlert("Video grabado", "Ubicacion: " + file.Path, "OK");
                rutaDeVideo = file.Path;
                file.Dispose();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Permiso denegado", "Da permisos de cámara al dispositivo.\nError: " + ex.Message, "Ok");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            PhotoPath = newFile;
        }

        public async void SaveVideoRecord()
        {
            var videos = new Models.VideoModel
            {
                VideoUri = PhotoPath,
                VideoDescripcion = txtDescripcion.Text
            };

            var resultado = await App.BaseDatosObject.SaveVideoRecord(videos);

            if (resultado == 1)
            {
                await DisplayAlert("", "Video Guardado.", "ok");
                txtDescripcion.Text = "";
                videoPlayer.Source = null;
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Guardar", "ok");
            }
        }

    }
}