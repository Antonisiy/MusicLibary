using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.DataModel
{
    public class ArtistGenre
    {
        public string ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public string GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
