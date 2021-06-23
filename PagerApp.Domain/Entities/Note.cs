using PagerApp.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PagerApp.Domain.Entities
{
    public class Note
    {
        public long Id { get; set; }
        [Required] public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateEdition { get; set; }
        [Required] public PriorityEnum Priority { get; set; }
    }
}
