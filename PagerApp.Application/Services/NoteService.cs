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

        public IEnumerable<NoteViewModel> GetNotes(Func<NoteViewModel, bool> predicate)
        {
            return noteRepository.GetNotes().Select(x => new NoteViewModel(x)).Where(predicate);
        }

        public IEnumerable<NoteViewModel> GetNotesAscOrderedByPriority()
        {
            return noteRepository.GetNotes().OrderBy(x => x.Priority).Select(x => new NoteViewModel(x));
        }

        public IEnumerable<NoteViewModel> GetNotesDescOrderedByPriority()
        {
            return noteRepository.GetNotes().OrderByDescending(x => x.Priority).Select(x => new NoteViewModel(x));
        }

        public IEnumerable<NoteViewModel> GetNotesSearchBy(SearchColumnEnum searchByEnum, string searchString)
        {
            return searchByEnum == SearchColumnEnum.Title 
                ? noteRepository.GetNotes().Where(x => x.Title.Contains(searchString)).Select(x => new NoteViewModel(x)) 
                : searchByEnum == SearchColumnEnum.Description 
                ? noteRepository.GetNotes().Where(x => !string.IsNullOrEmpty(x.Description) && x.Description.Contains(searchString)).Select(x => new NoteViewModel(x)) 
                : null;
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
