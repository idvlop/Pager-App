using Microsoft.EntityFrameworkCore;
using PagerApp.Data.Context;
using PagerApp.Domain.Entities;
using PagerApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PagerApp.Data.RepositoriesImpl
{
    public class NoteRepository : INoteRepository
    {
        private readonly PagerAppDbContext context;

        public NoteRepository(PagerAppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Note> GetNotes()
        {
            return context.Notes;
        }

        public IEnumerable<Note> GetNotes(Func<Note, bool> predicate)
        {
            return context.Notes.Where(predicate);
        }

        public Note GetOneById(long id)
        {
            return context.Notes.Find(id);
        }

        public void CreateNote(Note note)
        {
            note.DateCreation = DateTime.Now;
            context.Notes.Add(note);
            context.SaveChanges();
        }

        public void DeleteNote(Note note)
        {
            context.Entry(note).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            note.DateEdition = DateTime.Now;
            context.Entry(note).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
