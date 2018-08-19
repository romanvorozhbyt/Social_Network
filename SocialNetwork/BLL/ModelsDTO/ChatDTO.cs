using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsDTO
{
    public class ChatDTO:EntityDTO
    {

        public virtual ICollection<MessageDTO> Messages { get; set; }
        public virtual ICollection<UserDetailsDTO> Users { get; set; }
    }
}
