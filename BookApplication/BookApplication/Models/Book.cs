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

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}