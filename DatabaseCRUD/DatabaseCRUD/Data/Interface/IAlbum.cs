using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.DataModel.Interface
{
    public interface IAlbum
    {
        string Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime? DateRelease { get; set; }
        string ArtistId { get; set; }
        Artist Artist { get; set; }
        ObservableCollection<Song> Songs { get; set; }
        string ToString();
    }
}
