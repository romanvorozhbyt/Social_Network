using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace DAL.Models
{
   public class UserDetails 
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public  string FirstName { get; set; }
        [Required]
        [MaxLength(150)]
        public  string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(85)]
        public  string Country { get; set; }
        [MaxLength(58)]
        public  string City { get; set; }
        public string Address { get; set; }
        [Url]
        public string WebSite { get; set; }

        public virtual ICollection<UserDetails> Friends{ get; set; }
        public virtual ICollection<Chat> Chats{ get; set; }
        [ForeignKey("UserFromId")]
        public virtual ICollection<Message> Messages { get; set; }
        [ForeignKey("RequestedBy_Id")] 
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }

    }
}
