using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class PersistenceManager
    {
        public List<Photo> Load()
        {
            var query = from photo in PhotoDataContext.Current.Photos
                        select photo;
            return query.ToList();
        }
        public void Add(Photo photo)
        {
            PhotoDataContext.Current.Photos.InsertOnSubmit(photo);
            PhotoDataContext.Current.SubmitChanges();
        }
    }
}
