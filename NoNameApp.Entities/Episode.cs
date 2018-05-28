using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        [Display(Name = "Серия:   ")]
        public int NumberOfEpisode { get; set; }
        [Display(Name = "Дата выхода серии: ")]
        public DateTime Date { get; set; }
        public bool Wacthed { get; set; }
        [Display(Name = "Название: ")]
        public string Name { get; set; }
        [Display(Name = "Ссылка на серию: ")]
        public string Video { get; set; }


        public virtual Season Season { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Episode()
        {
            Ratings = new List<Rating>();
            Users = new List<User>();
        }
        

    }
}
