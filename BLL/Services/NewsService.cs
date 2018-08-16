using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstraction;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    class NewsService :INewsService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<NewsDTO> GetAll(int pageIndex, int pagesize)
        {
            var news = _db.News.GetAll()
                .OrderByDescending(n=>n.CreateTime)
                .Skip((pageIndex - 1) * pagesize)
                .Take(pagesize);
            return _mapper.Map<IEnumerable<NewsDTO>>(news);
        }

        
        public NewsDTO GetById(int id)
        {
            var news = _db.News.GetById(id);
            return _mapper.Map<NewsDTO>(news);
        }

        public void Insert(NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            _db.News.Insert(news);
            _db.Save();
        }

        public void Delete(string id)
        {
            _db.News.Delete(id);
            _db.Save();
        }

        public void Update(NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            _db.News.Update(news);
            _db.Save();
        }
    }
}
