using NoNameApp.DalContracts;
using NoNameApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Serial> Serials { get; }
        IRepository<News> News { get; }
        IRepository<User> Users { get; }
        IRepositoryAsync<User> Users2 {get;}
        void Save();
    }
}
