using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MLWebTest.Controllers
{
    public class ArtistsController : ApiController
    {
        // GET: api/Artists
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Artists/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Artists
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Artists/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Artists/5
        public void Delete(int id)
        {
        }
    }
}
