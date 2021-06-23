using PagerApp.Application.Enums;
using PagerApp.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.Application.Interfaces
{
    public interface INoteService
    {
        IEnumerable<NoteViewModel> GetNotes();

        NoteViewModel GetNote(long id);

        void AddNote(NoteViewModel noteVM);

        void UpdateNote(NoteViewModel noteVM);

        void DeleteNote(NoteViewModel noteVM);

        IEnumerable<NoteViewModel> GetNotesAscOrderedByPriority();

        IEnumerable<NoteViewModel> GetNotesDescOrderedByPriority();

        IEnumerable<NoteViewModel> GetFilteredNotes(string searchString, SearchColumnEnum searchColumn);
        IEnumerable<NoteViewModel> GetFilteredNotes(string searchStringTitle, string searchStringDescription, SearchColumnEnum searchColumn);

        IEnumerable<NoteViewModel> GetOrderedNotes(IEnumerable<NoteViewModel> noteViewModels, OrderColumnEnum orderColumn);
    }
}
