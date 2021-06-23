using PagerApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PagerApp.Domain.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes();
        IEnumerable<Note> GetNotes(Func<Note, bool> predicate);
        Note GetOneById(long id);
        void CreateNote(Note note);
        void DeleteNote(Note note);
        void UpdateNote(Note note);
    }
}
