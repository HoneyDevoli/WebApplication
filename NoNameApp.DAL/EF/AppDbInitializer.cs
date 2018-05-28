using System.Data.Entity;
using NoNameApp.Entities;
using System;
using System.IO;

namespace NoNameApp.DAL.EF
{
    internal class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext db)
        {
            db.Serials.Add(new Serial { Name = "WalkingDead",Begin=DateTime.Now, Country="USA", Duration=50, Picture= "/Img/WalkingDead.jpg", Description = "После зомби-апокалипсиса большая часть населения земного шара превратилась в живых мертвецов, жаждущих крови. Шериф Рик Граймс делает все, чтобы спасти в этих, в прямом смысле слова, нечеловеческих условиях дорогих ему людей… ", Status = true });
            db.Seasons.Add(new Season { NumberOfSeasons = 1});
            
            db.Episodes.Add(new Episode { NumberOfEpisode = 1, Date = DateTime.Now,  Name ="The Mother Lode" });
            db.Episodes.Add(new Episode { NumberOfEpisode = 2, Date = DateTime.Now,  Name= " Killing Your Number" });
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });
            db.Users.Add(new User
            {
                Email = "alice@gmail.com",
                Password = "123456",
                RoleId = 1
            });
            db.Users.Add(new User
            {
                Email = "tom@gmail.com",
                Password = "123456",
                RoleId = 2
            });
            db.SaveChanges();
            base.Seed(db);
        }
    }
}