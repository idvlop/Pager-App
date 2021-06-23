using PagerApp.Application.Enums;
using PagerApp.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagerApp.MVC.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(string titleFilter, string descriptionFilter, IEnumerable<NoteViewModel> noteViewModels)
        {
            TitleFilter = titleFilter;
            DescriptionFilter = descriptionFilter;
            SearchColumn = SearchColumnEnum.TitleAndDescription;
            NoteViewModels = noteViewModels;
        }

        public FilterViewModel(string stringFilter, SearchColumnEnum searchColumn, IEnumerable<NoteViewModel> noteViewModels)
        {
            switch (searchColumn)
            {
                case SearchColumnEnum.Title:
                    TitleFilter = stringFilter;
                    break;
                case SearchColumnEnum.Description:
                    DescriptionFilter = stringFilter;
                    break;
                case SearchColumnEnum.TitleOrDescription:
                    TitleAndDescriptionFilter = stringFilter;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            SearchColumn = searchColumn;
            NoteViewModels = noteViewModels;
        }

        public FilterViewModel(IEnumerable<NoteViewModel> noteViewModels)
        {
            NoteViewModels = noteViewModels;
        }

        public string TitleFilter { get; set; }
        public string DescriptionFilter { get; set; }
        public string TitleAndDescriptionFilter { get; set; }
        public SearchColumnEnum SearchColumn { get; set; }
        public IEnumerable<NoteViewModel> NoteViewModels { get; set; }
    }
}
