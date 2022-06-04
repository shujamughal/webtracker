using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
using WebTracker.Data;
namespace WebTracker.Repositories
{
    public class ActionDataRepository : IActionDataRepository
    {
        private readonly ApplicationDbContext _context;
        public ActionDataRepository(ApplicationDbContext context) => _context = context;

        public bool AddActionData(ActionData actionData)
        {
            _context.ActionsData.Add(actionData);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteActionData(int id)
        {
            var actionData = _context.ActionsData
                            .Where(x => x.ActionDataId == id)
                            .FirstOrDefault();
            if (actionData != null)
            {
                _context.ActionsData.Remove(actionData);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ActionData> GetActionDataByActionId(int actionId)
        {
            return _context.ActionsData
                            .Where(x => x.ActionId == actionId)
                            .ToList();
        }

        public ActionData GetActionDataById(int id)
        {
            return _context.ActionsData.FirstOrDefault(a => a.ActionDataId == id);
        }

        public List<ActionData> GetAllActionData()
        {
            return _context.ActionsData.ToList();
        }

        public bool UpdateActionData(int id, ActionData actionData)
        {
            var actionDataToUpdate = _context.ActionsData.FirstOrDefault(a => a.ActionDataId == id);
            if (actionDataToUpdate != null)
            {
                actionDataToUpdate.Name = actionData.Name;
                actionDataToUpdate.Data = actionData.Data;
                actionDataToUpdate.ActionId = actionData.ActionId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}