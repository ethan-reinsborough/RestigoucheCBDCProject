using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class Cover
    {
        [Display(Name = "CoverID")]
        public int ID { get; set; }

        [StringLength(150, MinimumLength = 2)]
        [Required(ErrorMessage = "Cover Id is required.")]
        public string imageSelected { get; set; }

        [Required(ErrorMessage = "Print Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime printDate { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public Book Book { get; set; }
    }
}