using PagerApp.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagerApp.MVC.Models
{
    public class IndexViewModel
    {
        public IEnumerable<NoteViewModel> Notes { get; set; }
        public FilterViewModel FilterVM { get; set; }
        public SortViewModel SortVM { get; set; }
    }
}
