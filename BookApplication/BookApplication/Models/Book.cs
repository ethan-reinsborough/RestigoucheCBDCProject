using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(25, ErrorMessage = "Title must be at least 2 characters.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        [StringLength(50, ErrorMessage = "Author must be at least 2 characters.", MinimumLength = 2)]
        public string Author { get; set; }


        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Publication Date")]
        [Display(Name = "Publication Date")]
        //Books may have a scheduled publication date, so future dates are allowed.
        public DateTime PublicationDate { get; set; }

        public virtual ICollection<File> Cover { get; set; }
    }
}