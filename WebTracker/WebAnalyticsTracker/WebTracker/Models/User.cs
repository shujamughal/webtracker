using System;
namespace WebTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public int VisitCount { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public DateTime LastConnection { get; set; }
        public Address Location {get; set; }
        public Website Website { get; set; }
    }
}
