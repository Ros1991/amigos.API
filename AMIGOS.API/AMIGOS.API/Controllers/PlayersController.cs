using AMIGOS.API.Data;
using AMIGOS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AMIGOS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayersController : ApiController
    {
        // GET: api/Players
        public IEnumerable<Player> Get()
        {
            return Players.getAllPlayers(Url.Content("~/"));
        }

        // GET: api/Players/5
        public Player Get(int id)
        {
            return Players.getPlayerById(Url.Content("~/"), id);
        }
        
        [HttpPost]
        // POST: api/Players
        public void Post([FromBody]Player value)
        {
            Players.insertPlayer(value, Url.Content("~/"));
        }

        // PUT: api/Players/5
        public void Put(int id, [FromBody]Player value)
        {
            Players.updatePlayer(id, value, Url.Content("~/"));
        }

        // DELETE: api/Players/5
        public void Delete(int id)
        {
            Players.deletePlayer(id);
        }
    }
}
