using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebTracker.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public DateTime LastConnection { get; set; }
        public Address Location {get; set; } = new Address();
        public List<Website> Web { get; set; } = new List<Website>();
    }
}
