using AMIGOS.API.Data;
using AMIGOS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMIGOS.API.Controllers
{
    public class RankingController : ApiController
    {
        // GET: api/Ranking
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ranking/5
        public List<RankingPlayer> Get(int id)
        {
            if(id == 0)
            {
                return Ranking.getBySemester();
            }
            else
            {
                return Ranking.getByYear(id);
            }

        }

        // POST: api/Ranking
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Ranking/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ranking/5
        public void Delete(int id)
        {
        }
    }
}
