using System;
using System.Collections.Generic;
using Har.Domain.Components;

namespace Har.Domain.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime OnlineSince { get; set; }
        public string ShortDescription { get; set; }
        public IEnumerable<string> Technologies { get; set; }
    }
}