using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class Cart
    {
        //Will be the logged in user's ID from AspNetUsers table
        public string UserID { get; set; }

        //The ID of the book that the user has added to their cart
        public int BookID { get; set; }

    }
}