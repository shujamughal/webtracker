using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebTracker.Models
{
    public class Website
    {
        [Key]
        public int Id { get; set; }
        public string Web { get; set; }
        public int VisitCount { get; set; }
        public List<Flow> Flows = new List<Flow>();
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}