using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
