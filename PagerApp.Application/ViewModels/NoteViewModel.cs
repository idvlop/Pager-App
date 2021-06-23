using PagerApp.Domain.Entities;
using PagerApp.Domain.Enums;

namespace PagerApp.Application.ViewModels
{
    public class NoteViewModel
    {
        public NoteViewModel(Note note)
        {
            Note = note;
        }

        public NoteViewModel() 
        {
            Note = new Note();
        }

        public NoteViewModel(string title, string description, int priority)
        {
            Note = new Note
            {
                Title = title,
                Description = description,
                Priority = (PriorityEnum)priority
            };
        }

        public Note Note { get; set; }

        public void EditNoteValues(string title, string description)
        {
            Note.Title = title;
            Note.Description = description;
        }
    }
}
