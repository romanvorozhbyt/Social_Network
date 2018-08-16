using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;


namespace BLL.Abstraction
{
    public interface IUserService
    {
        IEnumerable<UserDetailsDTO> GetAll();
        //IQueryable<UserDetails> GetAll(Expression<Func<UserDetails, bool>> predicate);
        UserDetailsDTO GetById(string id);
        void Insert(UserDetailsDTO user);
        void Delete(string id);
        void Update(UserDetailsDTO user);
    }
}
