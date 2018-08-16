using System;
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

        //[Required]
        //[ForeignKey("UserFrom")]
        public Guid UserFromId { get; set; }
        public virtual UserDetails UserFrom { get; set; }

        //[Required]
        //[ForeignKey("UserTo")]
        public Guid UserToId { get; set; }
        public virtual UserDetails UserTo { get; set; }

        [Required]
        public  DateTime CreateDate { get; set; }
        public  DateTime ModifiedDate { get; set; }
        //[Required]
        //[ForeignKey("Content")]
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
    }
}
