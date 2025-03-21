using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.lab2.Repositories.Data
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> Repository<T>() where T : class;
		void SaveChanges();
		void Dispose();
	}
}
