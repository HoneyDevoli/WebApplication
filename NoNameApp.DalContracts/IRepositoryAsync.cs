using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.DalContracts
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> SearchAsync(T item);
        Task<T> FindEmailAsync(string item);
    }
}
