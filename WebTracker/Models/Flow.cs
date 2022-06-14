using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Flow
    {
        public int FlowId { get; set; }
        public List<Url> Urls { get; set; } = new List<Url>();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}