using BackEnd;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;

namespace ReporteroDigital
{
    public partial class Map : PhoneApplicationPage
    {
        private readonly PersistenceManager _persistenceManager;
        private readonly PushpinCreator _pushpinCreator;
        public Map()
        {
            InitializeComponent();
            _persistenceManager = new PersistenceManager();
            _pushpinCreator = new PushpinCreator();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var listOfPhotos = _persistenceManager.Load();
            if (listOfPhotos != null && listOfPhotos.Count > 0)
            {
                var listOfPushins = _pushpinCreator.Create(listOfPhotos);
                foreach (Pushpin item in listOfPushins)
                {
                    photoMap.Children.Add(item);
                }
            }
        }
    }
}