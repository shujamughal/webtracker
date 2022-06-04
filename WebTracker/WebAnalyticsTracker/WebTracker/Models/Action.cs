using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Action
    {

        public string Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        List<ActionData> Data { get; set; } = new List<ActionData>();

    }
}