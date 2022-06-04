using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
namespace WebTracker.Repositories
{
    public interface IUrlRepository
    {
        List<Url> GetAllUrls();
        Url GetUrlById(int id);
        bool AddUrl(Url url);
        bool DeleteWebsite(int id);
        bool UpdateUrl(int id, Url url);
    }
}