using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
namespace WebTracker.Repositories
{
    public interface IActionDataRepository
    {
        List<ActionData> GetAllActionData();
        Task<List<ActionData>> GetActionDataByActionId(int actionId);
        ActionData GetActionDataById(int id);
        bool AddActionData(ActionData actionData);
        bool UpdateActionData(int id, ActionData actionData);
        bool DeleteActionData(int id);
    }
}