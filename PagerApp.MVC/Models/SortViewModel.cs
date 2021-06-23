using PagerApp.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagerApp.MVC.Models
{
    public class SortViewModel
    {
        public SortViewModel(OrderColumnEnum orderColumn)
        {
            OrderColumn = orderColumn == OrderColumnEnum.PriorityAsc ? OrderColumnEnum.PriorityDesc : OrderColumnEnum.PriorityAsc;
        }

        public OrderColumnEnum OrderColumn { get; set; }
    }
}
