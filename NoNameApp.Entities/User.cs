
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Serial> Serials { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
        [Display(Name = "Рейтинг: ")]
        public virtual ICollection<Rating> Ratings { get; set; }

       

        public User()
        {
            Episodes = new List<Episode>();
            Serials = new List<Serial>();
            Ratings = new List<Rating>();
            Orders = new List<Order>();
            
        }

    }
}
