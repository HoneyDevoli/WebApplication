using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameApp.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int SerialId { get; set; }
        public int ValueRating { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Serial Serial { get; set; }
    }
}
