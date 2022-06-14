using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Url
    {
        public int UrlId { get; set; }
        public string WebUrl { get; set; } = "";
        public int LoadTime { get; set; }
        public List<Action> Actions = new List<Action>();

        public int FlowId { get; set; }
        public Flow Flow { get; set; }
    }
}