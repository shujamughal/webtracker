using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Data;
namespace WebTracker.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private readonly ApplicationDbContext _context;
        public ActionRepository(ApplicationDbContext context) => _context = context;
        public bool AddAction(Models.Action action)
        {
            _context.Actions.Add(action);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteAction(int id)
        {
            var action = _context.Actions
                        .Where(x => x.ActionId == id)
                        .FirstOrDefault();
            if (action != null)
            {
                _context.Actions.Remove(action);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Models.Action GetActionById(int id)
        {
            return _context.Actions.FirstOrDefault(a => a.ActionId == id);
        }

        public List<Models.Action> GetAllActions()
        {
            return _context.Actions.ToList();
        }

        public bool UpdateAction(int id, Models.Action action)
        {
            var actionToUpdate = _context.Actions.FirstOrDefault(a => a.ActionId == id);
            if (actionToUpdate != null)
            {
                actionToUpdate.Type = action.Type;
                actionToUpdate.Content = action.Content;
                actionToUpdate.UrlId = action.UrlId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}