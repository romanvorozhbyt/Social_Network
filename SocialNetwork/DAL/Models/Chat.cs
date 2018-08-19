using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Chat : Entity
    {
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<UserDetails> Users { get; set; }
   
    }
}
