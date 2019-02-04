using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Repositories
{
	public interface IRepository<T>
	{
		Task Create(T item);
		Task Delete(T item);
		Task Update(T item);
        Task<T> GetItem(string id);
	}
}
