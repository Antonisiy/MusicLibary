using DatabaseCRUD.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Interface
{
    public interface ISong
    {
        string Id { get; set; }
        string Title { get; set; }
        string AlbumId { get; set; }
        Album Album { get; set; }
        string ToString();
    }
}
