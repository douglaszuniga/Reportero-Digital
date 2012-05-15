using System;
using System.Windows;
using BackEnd;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace ReporteroDigital
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PersistenceManager _persistenceManager;
        private GeoLocationManager _geoLocationManager;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _persistenceManager = new PersistenceManager();
            _geoLocationManager = new GeoLocationManager();
        }

        private void NavidateTo(string uri)
        {
            NavigationService.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        private void BtnViewInMapClick(object sender, RoutedEventArgs e)
        {
            NavidateTo("/Map.xaml");
        }

        private void BtnTakePictureClick(object sender, RoutedEventArgs e)
        {
            var captureTask = new CameraCaptureTask();
            _geoLocationManager.RetrieveCurrentLocation();
            captureTask.Completed += (s, a) =>
                                         {
                                             try
                                             {
                                                 if (a.Error == null && a.TaskResult == TaskResult.OK && a.ChosenPhoto != null)
                                                 {
                                                     var photo = new Photo
                                                                     {
                                                                         Latitude = _geoLocationManager.Latitude,
                                                                         Longitude = _geoLocationManager.Longitude
                                                                     };
                                                     var bytes = new byte[a.ChosenPhoto.Length];
                                                     a.ChosenPhoto.Read(bytes, 0, bytes.Length);
                                                     a.ChosenPhoto.Close();
                                                     photo.ImageBytes = bytes;

                                                     _persistenceManager.Add(photo);
                                                 }
                                             }
                                             catch (Exception ex)
                                             {
                                                Console.WriteLine(ex.Message);
                                             }
                                         };
            captureTask.Show();
        }
    }
}