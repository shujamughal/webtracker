using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
using WebTracker.Data;
namespace WebTracker.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationDbContext _context;
        public UrlRepository(ApplicationDbContext context) => _context = context;
        public bool AddUrl(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteWebsite(int id)
        {
            var url = _context.Urls
                        .Where(x => x.UrlId == id)
                        .FirstOrDefault();
            if (url != null)
            {
                _context.Urls.Remove(url);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Url> GetAllUrls()
        {
            return _context.Urls.ToList();
        }

        public Url GetUrlById(int id)
        {
            return _context.Urls.FirstOrDefault(u => u.UrlId == id);
        }

        public bool UpdateUrl(int id, Url url)
        {
            var urlToUpdate = _context.Urls.FirstOrDefault(u => u.UrlId == id);
            if (urlToUpdate != null)
            {
                urlToUpdate.WebUrl = url.WebUrl;
                urlToUpdate.LoadTime = url.LoadTime;
                urlToUpdate.FlowId = url.FlowId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}