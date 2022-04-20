using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class Action
    {
        [Key]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public List<ActionData> Data { get; set; } = new List<ActionData>();
        [ForeignKey("Url")]
        public int UrlId { get; set; }
        public virtual Url Url { get; set; }
    }
}