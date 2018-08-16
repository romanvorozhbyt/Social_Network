using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Friends:Entity
    {
        public Guid MeId { get; set; }
        public UserDetails Me { get; set; }

        public  Guid FriendId { get; set; }
        public  UserDetails Friend { get; set; }

        public string UserGroup { get; set; }
        public  string Status { get; set; }
    }
}
