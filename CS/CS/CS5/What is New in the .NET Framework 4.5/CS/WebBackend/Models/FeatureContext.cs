using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebBackend.Models
{
    public class FeatureContext : DbContext
    {
        public DbSet<Feature> Features { get; set; }
    }
}