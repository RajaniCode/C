using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBackend.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Image { get; set; }
        public int Rating { get; set; }

    }
}