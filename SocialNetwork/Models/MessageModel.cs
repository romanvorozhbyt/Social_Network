using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class MessageModel
    {

        public int ChatId { get; set; }
        
        public string UserFromId { get; set; }
        
        public  string Content { get; set; }
    }
}