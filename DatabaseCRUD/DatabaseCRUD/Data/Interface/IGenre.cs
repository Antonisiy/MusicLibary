using DatabaseCRUD.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Interface
{
    public interface IGenre
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ObservableCollection<ArtistGenre> ArtistGenres { get; set; }
        string ToString();
    }
}
