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
        public ActionResult Index(bool isAscOrder, SearchColumnEnum searchByEnum, string searchString)
        {
            isAscOrder = !isAscOrder;
            ViewBag.isAscOrder = isAscOrder;

            IEnumerable<NoteViewModel> noteVMs;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                noteVMs = noteService.GetNotesSearchBy(searchByEnum, searchString);
            }
            else
            {
                noteVMs = isAscOrder ? noteService.GetNotesAscOrderedByPriority() : noteService.GetNotesDescOrderedByPriority();
            }
            return View(noteVMs);
        }

        // GET: NoteController/Details/5
        public ActionResult Details(long id)
        {
            NoteViewModel noteVM = noteService.GetNote(id);
            return View(noteVM);
        }

        // GET: NoteController/Create
        public ActionResult Create(long id)
        {
            ViewBag.NoteId = id;
            return View();
        }

        // POST: NoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(collection["Title"])) throw new ArgumentException("Заголовок не может быть пустым.");

                var noteVM = new NoteViewModel(collection["Title"], collection["Description"], collection["Priority"]);
                noteService.AddNote(noteVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException exc)
            {
                return View("Error", new ErrorViewModel() { StatusCode = "400", ErrorMessage = exc.Message });
            }
        }

        // GET: NoteController/Edit/5
        public ActionResult Edit(long id)
        {
            NoteViewModel noteVM = noteService.GetNote(id);
            return View(noteVM);
        }

        // POST: NoteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(collection["Title"])) throw new ArgumentException("Заголовок не может быть пустым.");

                var noteVM = noteService.GetNote(id);
                noteVM.SetValues(collection["Title"], collection["Description"]);
                noteService.UpdateNote(noteVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException exc)
            {
                return View("Error", new ErrorViewModel() { StatusCode = "400", ErrorMessage = exc.Message });
            }
        }

        // GET: NoteController/Delete/5
        public ActionResult Delete(long id)
        {
            var noteVM = noteService.GetNote(id);
            return View(noteVM);
        }

        // POST: NoteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, IFormCollection collection)
        {
            try
            {
                var noteVM = noteService.GetNote(id);
                noteService.DeleteNote(noteVM);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }
    }
}
