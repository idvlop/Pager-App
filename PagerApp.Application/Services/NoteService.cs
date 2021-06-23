using PagerApp.Application.Enums;
using PagerApp.Application.Interfaces;
using PagerApp.Application.ViewModels;
using PagerApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.Application.Services
{
    public class NoteService : INoteService
    {
        private INoteRepository noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public void DeleteNote(NoteViewModel noteVM)
        {
            noteRepository.DeleteNote(noteVM.Note);
        }

        public IEnumerable<NoteViewModel> GetFilteredNotes(string searchString, SearchColumnEnum searchColumn)
        {
            var stringComparison = StringComparison.OrdinalIgnoreCase;
            return !string.IsNullOrWhiteSpace(searchString) && searchColumn == SearchColumnEnum.TitleOrDescription
                ? noteRepository.GetNotes(x => (x.Title.Contains(searchString, stringComparison) || (!string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(searchString, stringComparison)))).Select(x => new NoteViewModel(x))
                : null;
        }

        public IEnumerable<NoteViewModel> GetFilteredNotes(string searchStringTitle, string searchStringDescription, SearchColumnEnum searchColumn)
        {
            var stringComparison = StringComparison.OrdinalIgnoreCase;
            return !string.IsNullOrWhiteSpace(searchStringTitle) && !string.IsNullOrWhiteSpace(searchStringDescription) && searchColumn == SearchColumnEnum.TitleAndDescription
                ? noteRepository.GetNotes(x => (x.Title.Contains(searchStringTitle, stringComparison) && !string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(searchStringDescription, stringComparison))).Select(x => new NoteViewModel(x))
                : !string.IsNullOrWhiteSpace(searchStringTitle) && searchColumn == SearchColumnEnum.Title
                ? noteRepository.GetNotes(x => x.Title.Contains(searchStringTitle, stringComparison)).Select(x => new NoteViewModel(x))
                : !string.IsNullOrWhiteSpace(searchStringDescription) && searchColumn == SearchColumnEnum.Description
                ? noteRepository.GetNotes(x => (!string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(searchStringDescription, stringComparison))).Select(x => new NoteViewModel(x))
                : null;
        }

        public IEnumerable<NoteViewModel> GetOrderedNotes(IEnumerable<NoteViewModel> noteViewModels, OrderColumnEnum orderColumn)
        {
            return orderColumn == OrderColumnEnum.PriorityAsc
                ? noteViewModels.OrderBy(x => x.Note.Priority)
                : orderColumn == OrderColumnEnum.PriorityDesc
                ? noteViewModels.OrderByDescending(x => x.Note.Priority)
                : null;
        }

        public IEnumerable<NoteViewModel> GetNotesAscOrderedByPriority()
        {
            return noteRepository.GetNotes().OrderBy(x => x.Priority).Select(x => new NoteViewModel(x));
        }

        public IEnumerable<NoteViewModel> GetNotesDescOrderedByPriority()
        {
            return noteRepository.GetNotes().OrderByDescending(x => x.Priority).Select(x => new NoteViewModel(x));
        }

        void INoteService.AddNote(NoteViewModel noteVM)
        {
            noteRepository.CreateNote(noteVM.Note);
        }

        NoteViewModel INoteService.GetNote(long id)
        {
            return new NoteViewModel(noteRepository.GetOneById(id));
        }

        IEnumerable<NoteViewModel> INoteService.GetNotes()
        {
            return noteRepository.GetNotes().Select(x => new NoteViewModel(x));
        }

        void INoteService.UpdateNote(NoteViewModel noteVM)
        {
            noteRepository.UpdateNote(noteVM.Note);
        }
    }
}
