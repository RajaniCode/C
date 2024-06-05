using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBackend.Models
{
    public class FeatureSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
    }
}