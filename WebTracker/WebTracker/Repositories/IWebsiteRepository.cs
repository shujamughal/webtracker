using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;

namespace WebTracker.Repositories
{
    public interface IWebsiteRepository
    {
        List<Website> GetAllWebsites();
        Website GetWebsiteById(int id);
        int GetWebsiteIdByName(string website);
        bool AddWebsite(Website website);
        bool DeleteWebsite(int id);
        bool UpdateWebsite(int id, Website website);
    }
}