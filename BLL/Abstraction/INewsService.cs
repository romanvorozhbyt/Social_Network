using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace BLL.Abstraction
{
    public interface INewsService
    {
        IEnumerable<NewsDTO> GetAll(int pageIndex, int pagesize);
        NewsDTO GetById(int id);
        void Insert(NewsDTO news);
        void Delete(string id);
        void Update(NewsDTO news);
    }
}
