using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
using WebTracker.Data;

namespace WebTracker.Repositories
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly ApplicationDbContext _context;
        public WebsiteRepository(ApplicationDbContext context) => _context = context;
        public bool AddWebsite(Website website)
        {
            _context.Websites.Add(website);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteWebsite(int id)
        {
            var website = _context.Websites
                        .Where(x => x.WebsiteId == id)
                        .FirstOrDefault();
            if (website != null)
            {
                _context.Websites.Remove(website);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Website> GetAllWebsites()
        {
            List<Website> websites = _context.Websites.ToList();
            foreach(var website in websites)
            {
                website.Users = _context.Users.Where(x => x.WebsiteId == website.WebsiteId).ToList();
                foreach(var user in website.Users)
                {
                    user.Website = null;
                    user.Flows = _context.Flows.Where(x => x.UserId == user.UserId).ToList();
                    foreach(var flow in user.Flows)
                    {
                        flow.User = null;
                        flow.Urls = _context.Urls.Where(x => x.FlowId == flow.FlowId).ToList();
                        foreach(var url in flow.Urls)
                        {
                            url.Flow = null;
                            url.Actions = _context.Actions.Where(x => x.UrlId == url.UrlId).ToList();
                            foreach(var action in url.Actions)
                            {
                                action.Url = null;
                            }
                        }
                    }
                }
            }
            return websites;
        } 

        public Website GetWebsiteById(int id) 
        {
            var website = _context.Websites
                        .Where(x => x.WebsiteId == id)
                        .FirstOrDefault();
            if (website != null)
            {
                website.Users = _context.Users.Where(x => x.WebsiteId == website.WebsiteId).ToList();
                foreach(var user in website.Users)
                {
                    user.Website = null;
                    user.Flows = _context.Flows.Where(x => x.UserId == user.UserId).ToList();
                    foreach(var flow in user.Flows)
                    {
                        flow.User = null;
                        flow.Urls = _context.Urls.Where(x => x.FlowId == flow.FlowId).ToList();
                        foreach(var url in flow.Urls)
                        {
                            url.Flow = null;
                            url.Actions = _context.Actions.Where(x => x.UrlId == url.UrlId).ToList();
                            foreach(var action in url.Actions)
                            {
                                action.Url = null;
                            }
                        }
                    }
                }
            }
            return website;
        } 
        
        public int GetWebsiteIdByName(string website)
        {
            var web = _context.Websites.FirstOrDefault(w => w.Web == website);
            if(web != null)
            {
                return web.WebsiteId;
            }
            return -1;
        }

        public bool UpdateWebsite(int id, Website website)
        {
            var websiteToUpdate = _context.Websites.FirstOrDefault(w => w.WebsiteId == id);
            if(websiteToUpdate != null)
            {
                websiteToUpdate.Web = website.Web;
                websiteToUpdate.VisitCount = website.VisitCount;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}