using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class News : Entity
    {
        public Guid OwnerId { get; set; }
        public  UserDetails Owner { get; set; }

        [Required]
        public string Content { get; set; }
        public  string Photo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
