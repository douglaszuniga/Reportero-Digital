using System.Data.Linq.Mapping;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BackEnd
{
    [Table]
    public class Photo
    {   
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column(DbType = "image")]
        public byte[] ImageBytes { get; set; }
        [Column]
        public double Latitude { get; set; }
        [Column]
        public double Longitude { get; set; }

        public ImageBrush GetImageBrush()
        {
            var nBrush = new ImageBrush();
            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(new MemoryStream(ImageBytes, 0, ImageBytes.Length));
            nBrush.ImageSource = bitmapImage;
            return nBrush;
        }
    }
}
