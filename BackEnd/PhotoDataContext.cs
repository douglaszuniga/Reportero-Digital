using System.Data.Linq;

namespace BackEnd
{
    public class PhotoDataContext : DataContext
    {
        public Table<Photo> Photos;

        public PhotoDataContext(string connectionString) 
            :base(connectionString)
        {
            
        }
        static PhotoDataContext _dataContext;
        public static PhotoDataContext Current
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new PhotoDataContext("isostore:/reporteroDigital.sdf");
                    if (!_dataContext.DatabaseExists())
                    {
                        _dataContext.CreateDatabase();
                    }
                }
                return _dataContext;
            }
        }
    }
}
