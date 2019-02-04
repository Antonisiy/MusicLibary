using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using DatabaseCRUD.DataModel;

namespace DatabaseCRUD.Data.Repositories
{
	public interface IAlbumRepository : IRepository<Album>
	{
        ObservableCollection<Album> GetAllAlbums();
        ObservableCollection<Artist> GetAllArtist();
    }
}
