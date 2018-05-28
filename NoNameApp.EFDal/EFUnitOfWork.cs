using NoNameApp.Entities;
using NoNameApp.DAL.EF;
using System;
using NoNameApp.DalContracts;


namespace NoNameApp.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DAL.EF.AppContext db;
        private SerialRepository serialRepository;
        private NewsRepository newsRepository;
        private UserRepository userRepository;



        public EFUnitOfWork(string connectionString)
        {
            db = new DAL.EF.AppContext(connectionString);
        }
        public IRepository<Serial> Serials
        {
            get
            {
                if (serialRepository == null)
                    serialRepository = new SerialRepository(db);
                return serialRepository;
            }
        }

        public IRepository<News> News
        {
            get
            {
                if (serialRepository == null)
                    serialRepository = new SerialRepository(db);
                return newsRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepositoryAsync<User> Users2
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
