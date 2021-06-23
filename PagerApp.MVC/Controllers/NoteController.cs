using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagerApp.Application.Enums;
using PagerApp.Application.Interfaces;
using PagerApp.Application.ViewModels;
using PagerApp.Domain.Enums;
using PagerApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagerApp.MVC.Controllers
{
    public class NoteController : Controller
    {
        private INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        // GET: NoteController
        public ActionResult Index(string searchTitle, string searchDescription, string searchTitleAndDescription, OrderColumnEnum orderColumn = OrderColumnEnum.PriorityAsc)
        {
            ViewBag.searchTitle = searchTitle;
            ViewBag.searchDescription = searchDescription;
            ViewBag.searchTitleAndDescription = searchTitleAndDescription;

            IEnumerable<NoteViewModel> noteVMs;
            FilterViewModel filterVM;
            SortViewModel sortVM;


            if (!string.IsNullOrWhiteSpace(searchTitle) && !string.IsNullOrWhiteSpace(searchDescription))
            {
                noteVMs = noteService.GetFilteredNotes(searchTitle, searchDescription, SearchColumnEnum.TitleAndDescription);
                filterVM = new FilterViewModel(searchTitle, searchDescription, noteVMs);
            }
            else if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                noteVMs = noteService.GetFilteredNotes(searchTitle, searchDescription, SearchColumnEnum.Title);
                filterVM = new FilterViewModel(searchTitle, SearchColumnEnum.Title, noteVMs);
            }
            else if (!string.IsNullOrWhiteSpace(searchDescription))
            {
                noteVMs = noteService.GetFilteredNotes(searchTitle, searchDescription, SearchColumnEnum.Description);
                filterVM = new FilterViewModel(searchDescription, SearchColumnEnum.Description, noteVMs);
            }
            else if (!string.IsNullOrWhiteSpace(searchTitleAndDescription))
            {
                noteVMs = noteService.GetFilteredNotes(searchTitleAndDescription, SearchColumnEnum.TitleOrDescription);
                filterVM = new FilterViewModel(searchTitleAndDescription, SearchColumnEnum.TitleOrDescription, noteVMs);
            }
            else
            {
                noteVMs = noteService.GetNotes();
                filterVM = new FilterViewModel(noteVMs);
            }

            noteVMs = orderColumn == OrderColumnEnum.PriorityAsc ? noteService.GetOrderedNotes(noteVMs, OrderColumnEnum.PriorityAsc) : noteService.GetOrderedNotes(noteVMs, OrderColumnEnum.PriorityDesc);
            sortVM = new SortViewModel(orderColumn);
            var indexVM = new IndexViewModel()
            {
                FilterVM = filterVM,
                SortVM = sortVM,
                Notes = noteVMs
            };
            return View(indexVM);
        }

        // GET: NoteController/Details/5
        public ActionResult Details(long id)
        {
            try
            {
                NoteViewModel noteVM = noteService.GetNote(id);

                if (noteVM.Note == null) throw new ArgumentException($"Ошибка: заметки с id = {id} не существует.");

                return View(noteVM);
            }
            catch(ArgumentException exc)
            {
                return StatusCode(404, exc.Message);
            }
        }

        // GET: NoteController/Create
        public ActionResult Create()
        {
            return View(new NoteFormViewModel());
        }

        // POST: NoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteFormViewModel noteFormVM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(noteFormVM.Title)) throw new ArgumentException("Ошибка: заголовок не может быть пустым.");

                var noteVM = new NoteViewModel(noteFormVM.Title, noteFormVM.Description, noteFormVM.Priority);
                noteService.AddNote(noteVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException exc)
            {
                return StatusCode(400, exc.Message);
                //return View("Error", new ErrorViewModel() { StatusCode = "400", ErrorMessage = exc.Message });
            }
        }

        // GET: NoteController/Edit/5
        public ActionResult Edit(long id)
        {
            try
            {
                var noteVM = noteService.GetNote(id);
                if (noteVM.Note == null) throw new BadHttpRequestException($"Ошибка: заметки с id = {id} не существует.");

                return View(new NoteFormViewModel() { Title = noteVM.Note.Title, Description = noteVM.Note.Description });
            }
            catch (BadHttpRequestException exc)
            {
                return StatusCode(404, exc.Message);
            }
        }

        // POST: NoteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, NoteFormViewModel noteFormVM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(noteFormVM.Title)) throw new ArgumentException("Ошибка: заголовок не может быть пустым.");

                var noteVM = noteService.GetNote(id);

                if (noteVM.Note == null) throw new BadHttpRequestException($"Ошибка: заметки с id = {id} не существует.");

                noteVM.EditNoteValues(noteFormVM.Title, noteFormVM.Description);
                noteService.UpdateNote(noteVM);
                return RedirectToAction(nameof(Index));
        }
            catch (ArgumentException exc)
            {
                return StatusCode(400, exc.Message);
                //return View("Error", new ErrorViewModel() { StatusCode = "404", ErrorMessage = exc.Message });
            }
            catch (BadHttpRequestException exc)
            {
                return StatusCode(404, exc.Message);
            }
        }

        // GET: NoteController/Delete/5
        public ActionResult Delete(long id)
        {
            try
            {
                NoteViewModel noteVM = noteService.GetNote(id);

                if (noteVM.Note == null) throw new ArgumentException($"Ошибка: заметки с id = {id} не существует.");

                return View(noteVM);
            }
            catch (ArgumentException exc)
            {
                return StatusCode(404, exc.Message);
            }
        }

        // POST: NoteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, NoteFormViewModel noteFormVM)
        {
            try
            {
                var noteVM = noteService.GetNote(id);

                if (noteVM.Note == null) throw new BadHttpRequestException($"Ошибка: заметки с id = {id} не существует.");

                noteService.DeleteNote(noteVM);
                return RedirectToAction(nameof(Index));
            }
            catch(BadHttpRequestException exc)
            {
                return StatusCode(404, exc.Message);
            }
        }
    }
}
