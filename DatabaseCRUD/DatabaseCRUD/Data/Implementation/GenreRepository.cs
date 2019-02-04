using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Implementation
{
	public class GenreRepository : IGenreRepository
	{
		private DataContext context;
		public GenreRepository(DataContext context)
		{
			this.context = context;	
		}
		public async Task Create(Genre item)
		{
			await context.Genres.AddAsync(item);
            context.SaveChanges();
			return;
		}

		public async Task Delete(Genre item)
		{
			context.Genres.Remove(item);
			await context.SaveChangesAsync();
			return;
		}

		public async Task<Genre> GetItem(string id)
		{
			var res = await context.Genres.FindAsync(id);
			return res;
		}
        public async Task Update(Genre item)
		{
            context.Genres.Update(item);
			await context.SaveChangesAsync();
			return;
		}
        public ObservableCollection<Genre> GetAllGenres()
        {
            return new ObservableCollection<Genre>(context.Genres.ToList());
        }
    }
}
