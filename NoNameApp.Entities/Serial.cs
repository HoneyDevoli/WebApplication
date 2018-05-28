using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoNameApp.Entities
{
    public class Serial
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название сериала")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string Picture { get; set; }

        [Required]
        [Display(Name = "Даты выхода")]
        public DateTime Begin { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Range(1, 300, ErrorMessage = "Недопустимая длительность серии")]
        [Display(Name = "Длительность серии")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public bool Status { get; set; }


        public virtual ICollection<User> Users { get; set; }
        [Display(Name = "Рейтинг")]
        public virtual ICollection<Rating> Ratings { get; set; }
        [Display(Name = "Сезон")]
        public virtual ICollection<Season> Seasons { get; set; }

        public Serial()
        {
            Users = new List<User>();
            Ratings = new List<Rating>();
            Seasons = new List<Season>();
        }
    }
}
