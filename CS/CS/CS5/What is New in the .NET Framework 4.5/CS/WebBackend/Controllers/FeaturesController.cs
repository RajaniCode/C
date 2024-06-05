using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebBackend.Models;

namespace WebBackend.Controllers
{
    public class FeaturesController : ApiController
    {

        private FeatureContext db = new FeatureContext();

        public IEnumerable<Feature> Get()
        {
            return db.Features.OrderBy(f => f.Id);
        }

        public HttpResponseMessage Get(int id)
        {
            var feature = db.Features.SingleOrDefault(f => f.Id == id);

            if (feature == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Feature not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, feature);
        }
    }
}
