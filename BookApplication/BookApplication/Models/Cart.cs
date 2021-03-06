using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class Cart
    {
        //Autogenerated ID to simplify routing
        public int ID { get; set; }

        //Will be the logged in user's ID from AspNetUsers table
        public string userID { get; set; }

        [Display(Name = "Title")]
        public string bookTitle { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        //The ID of the book that the user has added to their cart
        public int bookID { get; set; }

    }
}