using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using DatabaseCRUD.DataModel;

namespace DatabaseCRUD.Data.Repositories
{
	public interface IArtistRepository : IRepository<Artist>
	{
        ObservableCollection<Artist> GetAllArtists();
    }
}
