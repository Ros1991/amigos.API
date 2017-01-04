using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Models
{
    public class GameDay
    {
        public int id { get; set; }
        public string gameDate { get; set; }
        public IEnumerable<Game> jogosFinalizados { get; set; }
    }
}