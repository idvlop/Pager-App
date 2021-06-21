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

        IEnumerable<NoteViewModel> GetNotes(Func<NoteViewModel, bool> predicate);

        IEnumerable<NoteViewModel> GetNotesAscOrderedByPriority();

        IEnumerable<NoteViewModel> GetNotesDescOrderedByPriority();

        IEnumerable<NoteViewModel> GetNotesSearchBy(SearchColumnEnum searchByEnum, string searchString);

        NoteViewModel GetNote(long id);

        void AddNote(NoteViewModel noteVM);

        void UpdateNote(NoteViewModel noteVM);

        void DeleteNote(NoteViewModel noteVM);
    }
}
