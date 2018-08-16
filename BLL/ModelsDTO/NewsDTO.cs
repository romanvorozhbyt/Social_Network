using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsDTO
{
    public class NewsDTO : EntityDTO
    {

        public string OwnerId { get; set; }
        public UserDetailsDTO Owner { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
