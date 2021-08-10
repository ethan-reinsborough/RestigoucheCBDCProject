﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class File
    {
        [Key]
        public int FileID { get; set; }
        [StringLength(255)]

        public string FileName { get; set; }
        [StringLength(100)]

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public int ID { get; set; }

        public virtual Cover Cover { get; set; }
    }
}