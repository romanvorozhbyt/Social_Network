using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Content :Entity
    {
        [Key]
        [ForeignKey("Message")]
        public override int Id { get; set; }
        public string MessageContent { get; set; }
        public Message Message { get; set; }
    }
}
