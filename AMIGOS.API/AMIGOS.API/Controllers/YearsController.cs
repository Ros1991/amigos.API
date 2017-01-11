using AMIGOS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMIGOS.API.Controllers
{
    public class YearsController : ApiController
    {
        // GET: api/Years
        public IEnumerable<string> Get()
        {
            return Games.getAllYears();
        }

        // GET: api/Years/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Years
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Years/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Years/5
        public void Delete(int id)
        {
        }
    }
}
