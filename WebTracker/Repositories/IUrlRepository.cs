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
        Task<List<Url>> GetUrlsByFlowId(int flowId);
        Task<Url> GetUrlById(int id);
        bool AddUrl(Url url);
        bool DeleteUrl(int id);
        bool UpdateUrl(int id, Url url);
    }
}