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
        [Key]
        public int Id { get; set; }
        public string WebUrl { get; set; }  
        public List<Action> Actions = new List<Action>();
        [ForeignKey("Flow")]
        public int FlowId { get; set; }
        public virtual Flow Flow { get; set; }
    }
}