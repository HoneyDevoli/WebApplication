using NoNameApp.Entities;
using NoNameApp.BLL.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using NoNameApp.LogicContracts;
using NoNameApp.DAL.EF;
using System;
using System.Data.Entity;

namespace NoNameApp.Logic
{
    public class SerialService : ISerialService
    {
        private DAL.EF.AppContext db;
        public SerialService(DbContext context)
        {
            db = (DAL.EF.AppContext)context;
        }

        public Serial GetSerial(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id сериала", "");
            var serial = db.Serials.Find(id);
            if (serial == null)
                throw new ValidationException("Сериал не найден", "");
            return serial;
        }
        public IEnumerable<Serial> GetSerials()
        {
            return db.Serials.ToList();
        }
        

        public OperationDetails Delete(int id)
        {
            var serial= db.Serials.Find(id);
            if (serial == null)
                throw new ArgumentException("Сериал не может быть удален ", "");
            foreach (var seas in serial.Seasons.ToList())
            {
                foreach (var ep in seas.Episodes.ToList())
                {
                    db.Episodes.Remove(ep);
                }
                db.Seasons.Remove(seas);
            }
            db.Serials.Remove(serial);
            db.SaveChanges();
            return new OperationDetails(true, "Сериал удален", "");
        }

        public List<Serial> GetSearchSerials(string search)
        {
            if (search.Trim() == string.Empty )
                throw new ValidationException("По данному запросу ничего не найдено", "");
            var allSerials = db.Serials.ToList();
            var searchSerials = allSerials
                .Where(s => s.Name.ToLower().Contains(search.ToLower()))
                .ToList();
            if (searchSerials.Count == 0)
                throw new ValidationException("По данному запросу ничего не найдено", "");
            return searchSerials;
        }

        public OperationDetails Adding(string name, DateTime date, string pic, string country, string desc, int dur, bool status)
        {
            Serial addSerial = new Serial
            {
                Name = name,
                Begin = date,
                Picture = pic,
                Country = country,
                Description = desc,
                Duration = dur,
                Status = status

            };
            db.Serials.Add(addSerial);
            db.SaveChanges();
            return new OperationDetails(true, "Сериал добавлен", "");

        }

        public OperationDetails DeleteSeason(int id)
        {
            var season = db.Seasons.Find(id);
            if (season == null)
                throw new ValidationException("Не возможно удалить сезон", "");
            foreach (var ep in season.Episodes.ToList())
            {
                db.Episodes.Remove(ep);
            }
            db.Seasons.Remove(season);
            db.SaveChanges();
            return new OperationDetails(true, "Сезон удален", "");
        }

        public OperationDetails DeleteEpisode(int id)
        {
            var episode = db.Episodes.Find(id);
            if (episode == null)
                throw new ValidationException("Невозможно удалить эпизод", "");
            db.Episodes.Remove(episode);
            db.SaveChanges();
            return new OperationDetails(true, "Эпизод удален","");
        }
       
        public OperationDetails ChangeEpisode(int id, string name, int number)
        {
            var episode = db.Episodes.Find(id);
            if (name == String.Empty)
                throw new ValidationException("Имя сериала не может быть пустым","");
            if (number <0)
                throw new ValidationException("Значение номера серии не может быть меньше 1", "");
            if (name.Length <5)
                throw new ValidationException("Имя сериала не может быть меньше 6 символов", "");
            episode.Name = name;
            episode.NumberOfEpisode = number;
            db.Entry(episode).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "Данные эпизода изменены", "");
        }
        public OperationDetails ChangeSeason(int id, int number)
        {
            var season = db.Seasons.Find(id);
            if (season == null)
                throw new ValidationException("Невозможно найти сезон", "");
            if (number < 1)
                throw new ValidationException("Номер сезона не может быть отрицательным числом", "");
            season.NumberOfSeasons = number;
            db.Entry(season).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "Данные сезона изменены", "");
        }

        public OperationDetails ChangeDescription(int id, string description)
        {
            var serial = db.Serials.Find(id);
            if (serial == null)
                throw new ValidationException("Сериал не найден в базе", "");
            if( description.Length<20)
                throw new ValidationException("Описание должно быть боль 20 символов", "");
            serial.Description = description;
            db.Entry(serial).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "Описание сериала изменено", "");
        }
        
        public OperationDetails AddSeason(int id, int count)
        {
            Season addSeason = new Season
            {
                NumberOfSeasons = count + 1,
                SerialId = id

            };
            db.Seasons.Add(addSeason);
            db.SaveChanges();
            return new OperationDetails(true, "Cезон добавлен", "");
        }

        public OperationDetails AddEpisode(int id, int count, string name, DateTime? date)
        {
            if (name == String.Empty)
                throw new ValidationException("Имя сериала не может быть пустым", "");
            if (name.Length < 3)
                throw new ValidationException("Имя сериала не может быть меньше 3 символов", "");
            if (date == null)
                throw new ValidationException("Укажите дату", "");
            var temp = (DateTime)date;
            if ( temp.Year > 2100)
                throw new ValidationException("Неверно указан год", "");
            
            Episode addEpisode = new Episode
            {
                NumberOfEpisode = count + 1,
                Date = (DateTime)date,
                Wacthed = false,
                Name = name,
                Season = db.Seasons.Find(id)
            };
            db.Episodes.Add(addEpisode);
            db.SaveChanges();
            return new OperationDetails(true, "Эпизод добавлен", "");
        }

        public OperationDetails ChangeInfoSer(int id, DateTime? begin, string linkPic, string country, int duration, bool status)
        {
            var serial = db.Serials.Find(id);
            if(serial == null)
                throw new ValidationException("Сериал не найден в базе", "");
            if(country==String.Empty)
                throw new ValidationException("Заполните поле страна", "");
            if (duration < 1)
                throw new ValidationException("Длительность серии не может быть отрицательным числом","");
            if (begin == null)
                throw new ValidationException("Укажите дату", "");
            var temp = (DateTime)begin;
            if (temp.Year > 2100)
                throw new ValidationException("Неверно указан год", "");
            serial.Begin = temp;
            serial.Picture = linkPic;
            serial.Country = country;
            serial.Duration = duration;
            serial.Status = status;
            db.Entry(serial).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "Данные изменены", "");
        }

        public OperationDetails ChangeSerialName(int id, string name)
        {
            var serial = db.Serials.Find(id);
            if (serial == null)
                throw new ValidationException("Сериал не найден в базе", "");
            if (name == String.Empty)
                throw new ValidationException("Имя сериала не может быть пустым", "");
            serial.Name = name;
            db.Entry(serial).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "Имя сериала изменено", "");
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}

