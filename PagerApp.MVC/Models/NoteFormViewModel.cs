using PagerApp.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PagerApp.MVC.Models
{
    public class NoteFormViewModel
    {
        [Required(ErrorMessage = "Заголовок не может быть пустым")]
        [Display(Name = "Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Приоритет")]
        public int Priority { get; set; }
    }
}
