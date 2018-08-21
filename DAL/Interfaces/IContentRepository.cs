using System.Collections.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IContentRepository
    {
        Content GetById(int id);
        void Insert(Content item);
        void Delete(int id);
        void DeleteRange(IEnumerable<Content> contentList);
        void Update(Content item);
    }
}
