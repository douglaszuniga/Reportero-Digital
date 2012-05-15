using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls.Maps;

namespace BackEnd
{
    public class PushpinCreator
    {
        public Pushpin Create(ImageBrush imageBrush, GeoCoordinate geoCoordinate)
        {
            var pushpin = new Pushpin
                                  {
                                      Location = geoCoordinate,
                                      Content = new Image {Source = imageBrush.ImageSource, Stretch = Stretch.UniformToFill, Height = 75, Width = 75},
                                  };
            return pushpin;
        }

        public List<Pushpin> Create(List<Photo> photos)
        {
            return photos.Select(photo => Create(photo.GetImageBrush(), new GeoCoordinate(photo.Latitude, photo.Longitude))).ToList();
        }
    }
}
