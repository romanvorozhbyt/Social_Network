using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsDTO
{
   public class MessageDTO :EntityDTO
    {

        public int ChatId { get; set; }
        public ChatDTO Chat { get; set; }

        public string UserFromId { get; set; }
        public UserDetailsDTO UserFrom { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ContentDTO Content { get; set; }
    }
}
