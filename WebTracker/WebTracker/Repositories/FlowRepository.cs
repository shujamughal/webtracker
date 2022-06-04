using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTracker.Models;
using WebTracker.Data;
namespace WebTracker.Repositories
{
    public class FlowRepository :IFlowRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public bool AddFlow(Flow flow)
        {
            _context.Flows.Add(flow);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFlow(int id)
        {
            var flow = _context.Flows
                        .Where(x => x.FlowId == id)
                        .FirstOrDefault();
            if (flow != null)
            {
                _context.Flows.Remove(flow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Flow> GetAllFlows() => _context.Flows.ToList();

        public Flow GetFlowById(int id) => _context.Flows.FirstOrDefault(f => f.FlowId == id);

        public bool UpdateFlow(int id, Flow flow)
        {
            var flowToUpdate = _context.Flows
                        .Where(x => x.FlowId == id)
                        .FirstOrDefault();
            if (flowToUpdate != null)
            {
                flowToUpdate.Urls = flow.Urls;
                flowToUpdate.UserId = flow.UserId;
                flowToUpdate.User = flow.User;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}