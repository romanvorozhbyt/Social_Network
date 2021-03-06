﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Message :Entity
    {
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        [Required]
        [ForeignKey("UserFrom")]
        public string UserFromId { get; set; }
        public UserDetails UserFrom { get; set; }

        [Required]
        public  DateTime CreateDate { get; set; }
        public  DateTime ModifiedDate { get; set; }

        
        public virtual Content Content { get; set; }
    }
}
