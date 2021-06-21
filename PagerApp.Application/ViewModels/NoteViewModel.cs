using PagerApp.Domain.Entities;
using PagerApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.Application.ViewModels
{
    public class NoteViewModel
    {
        //public IEnumerable<Note> Notes { get; set; }
        public NoteViewModel(Note note)
        {
            Note = note;
        }

        public NoteViewModel() 
        {
            Note = new Note();
        }

        public NoteViewModel(string title, string description, string priority)
        {
            Note = new Note
            {
                Title = title,
                Description = description,
                Priority = (PriorityEnum)int.Parse(priority)
            };
        }


        public Note Note { get; set; }

        public void SetValues(string title, string description)
        {
            Note.Title = title;
            Note.Description = description;
        }
    }
}
