using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Entities
{
    public class Season
    {
        public int Id { get; set; }
        [Display(Name = "Сезон: ")]
        public int NumberOfSeasons { get; set; }

        public virtual Serial Serial { get; set; }
        public int SerialId { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }

        public Season()
        {
            Episodes = new List<Episode>();
        }


    }
}
