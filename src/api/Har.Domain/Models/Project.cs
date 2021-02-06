using System;

namespace Har.Domain.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime OnlineSince { get; set; }
        public string ShortDescription { get; set; }
        public string Technologies { get; set; }
    }
}