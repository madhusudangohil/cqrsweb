using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using cqrswebreaddataccess;

namespace cqrswebapi.Controllers
{
   
    public class HomeApiController : ApiController
    {
        public HttpResponseMessage Get(Guid id)
        {
            ReadDataContext rdc = new ReadDataContext("readStore");

            var tv = rdc.Query<Television>().FirstOrDefault(x => x.Id == id);

            return Request.CreateResponse(HttpStatusCode.OK, tv);
        }

        public HttpResponseMessage Get()
        {
            ReadDataContext rdc = new ReadDataContext("readStore");

            var tv = rdc.Query<Television>().ToList();

            return Request.CreateResponse(HttpStatusCode.OK, tv);
        }
    }
}