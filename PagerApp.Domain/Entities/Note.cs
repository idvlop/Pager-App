using PagerApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.Domain.Entities
{
    public class Note
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Заголовок не может быть пустым")]
        [Display(Name="Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Создана")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Последнего изменение")]
        [DataType(DataType.DateTime)]
        public DateTime? DateEdition { get; set; }

        [Display(Name = "Приоритет")]
        public PriorityEnum? Priority { get; set; }
    }
}
