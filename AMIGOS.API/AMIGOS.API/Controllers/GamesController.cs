using AMIGOS.API.Data;
using AMIGOS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AMIGOS.API.Controllers
{
    public class GamesController : ApiController
    {
        // GET: api/Games
        public IEnumerable<GameDay> Get()
        {
            return Games.getAllGames();
        }

        // GET: api/Games/5
        public IEnumerable<GameDay> Get(int id)
        {
            return Games.getAllGamesByYear(id);
        }

        // POST: api/Games
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Post([FromBody]GameDay value)
        {
            Games.insertGameDay(value);
        }

        // PUT: api/Games/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Games/5
        public void Delete(int id)
        {
        }
    }
}
