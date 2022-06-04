using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Flow
    {
        public int Id { get; set; }
        public List<Url> Urls { get; set; } = new List<Url>();
        public List<Action> Actions = new List<Action>();
    }
}