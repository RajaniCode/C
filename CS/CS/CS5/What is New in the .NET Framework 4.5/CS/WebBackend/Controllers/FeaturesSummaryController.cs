using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebBackend.Models;

namespace WebBackend.Controllers
{
    public class FeaturesSummaryController : ApiController
    {
        private FeatureContext db = new FeatureContext();

        public IEnumerable<int> Get()
        {
            return from f in db.Features
                   orderby f.Version descending, f.Name ascending
                   select f.Id;
        }

        public HttpResponseMessage Get(int id)
        {
            var feature = db.Features.SingleOrDefault(f => f.Id == id);

            if (feature == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Feature not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, new FeatureSummary { Id = feature.Id, Name = feature.Name, Icon = feature.Icon });
        }
    }
}
