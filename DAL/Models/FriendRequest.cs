using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class FriendRequest:Entity
    {
        [ForeignKey("RequestedBy")]
        public string RequestedBy_Id { get; set; }
        public virtual UserDetails RequestedBy { get; set; }

        public  virtual UserDetails RequestedTo { get; set; }

        public DateTime? RequestTime { get; set; }

        public FriendRequestFlag FriendRequestFlag { get; set; }
    }

    public enum FriendRequestFlag
    {
        Pending,
        Approved,
        Declined,
        Blocked,
        Spam
    };
}

