using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.DataModel.Interface
{
    public interface IArtist
    {
        string Id { get; set; }
        string Name { get; set; }
        string Biography{ get; set; }
        string Country { get; set; }
        ObservableCollection<ArtistGenre> ArtistGenres { get; set; }
        ObservableCollection<Album> Albums { get; set; }
    }
}
