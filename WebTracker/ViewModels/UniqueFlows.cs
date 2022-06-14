using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracker.Models;
namespace WebTracker.ViewModels
{
    public class UniqueFlows
    {
        public List<Url> flow { get; set; } = new List<Url>();
        int count;
    }
}