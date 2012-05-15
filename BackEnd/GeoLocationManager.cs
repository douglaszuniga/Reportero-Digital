using System;
using System.Device.Location;

namespace BackEnd
{
    public class GeoLocationManager
    {
        private readonly GeoCoordinateWatcher _watcher;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoLocationManager()
        {
            // using high accuracy;
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            _watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(WatcherStatusChanged);
            _watcher.PositionChanged += new System.EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(WatcherPositionChanged);
        }

        void WatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                _watcher.Stop();
                SetLocalLocation(_watcher.Position.Location);
            }
        }

        private void SetLocalLocation(GeoCoordinate location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        private void WatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            SetLocalLocation(e.Position.Location);
        }

        public bool RetrieveCurrentLocation()
        {
            return _watcher.TryStart(true, TimeSpan.FromMilliseconds(60000));
        }
    }
}
