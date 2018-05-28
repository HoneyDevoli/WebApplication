using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameApp.Entities;
using System.Data.Entity;
using NoNameApp.DalContracts;

namespace NoNameApp.Repository
{
    class NewsRepository:IRepository<News>
    {
        private DAL.EF.AppContext db;

        public NewsRepository(DAL.EF.AppContext context)
        {
            this.db = context;
        }

        public IEnumerable<News> GetAll()
        {
            return db.News;
        }

        public News Get(int id)
        {
            return db.News.Find(id);
        }

        public void Create(News news)
        {
            db.News.Add(news);
        }

        public void Update(News news)
        {
            db.Entry(news).State = EntityState.Modified;
        }

        public IEnumerable<News> Find(Func<News, Boolean> predicate)
        {
            return db.News.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            News news = db.News.Find(id);
            if (news != null)
                db.News.Remove(news);
        }
    }
}

