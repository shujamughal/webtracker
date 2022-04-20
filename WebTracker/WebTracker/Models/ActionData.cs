using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTracker.Models
{
    public class ActionData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}