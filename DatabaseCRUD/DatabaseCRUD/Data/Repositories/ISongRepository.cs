using DatabaseCRUD.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Repositories
{
    public interface ISongRepository : IRepository<Song>
    {
        ObservableCollection<Song> GetAllSongs();
        ObservableCollection<Album> GetAllAlbums(string idArtist);
    }
}
