using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IChatRepository
    {
        void Insert(Chat chat);
        void Delete(int id);
        Chat GetById(int id);

    }
}
