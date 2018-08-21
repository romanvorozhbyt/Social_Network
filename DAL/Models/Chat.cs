using System;
using System.Collections.Generic;


namespace DAL.Models
{
    public class Chat : Entity
    {
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<UserDetails> Users { get; set; }
   
    }
}
