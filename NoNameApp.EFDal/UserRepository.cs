using System;
using System.Collections.Generic;
using System.Linq;
using NoNameApp.Entities;
using System.Data.Entity;
using NoNameApp.DalContracts;
using System.Threading.Tasks;

namespace NoNameApp.Repository
{
    public class UserRepository : IRepositoryAsync<User>, IRepository<User>
    {
        private DAL.EF.AppContext db;

        public UserRepository(DAL.EF.AppContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public async Task<User> SearchAsync(User user)
        {
           return await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
        }

        public async Task<User> FindEmailAsync(String Email)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }
    }
}
