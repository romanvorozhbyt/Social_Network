﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Activation;




namespace BLL.ModelsDTO
{
    public class UserDetailsDTO 
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public virtual ICollection<ChatDTO> Chats { get; set; }

    }
}
