using DatabaseCRUD.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        ObservableCollection<Genre> GetAllGenres();
    }
}
