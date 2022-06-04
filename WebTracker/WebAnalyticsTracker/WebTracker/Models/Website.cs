using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Website
    {
        public int Id { get; set; }
        public string Web { get; set; }
        public List<Flow> Flows = new List<Flow>();
    }
}