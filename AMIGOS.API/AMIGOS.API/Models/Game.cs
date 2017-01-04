using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Models
{
    public class Game
    {
        public int id { get; set; }
        public int numDeJog { get; set; }
        public PlayerGame goleiroTime1 { get; set; }
        public PlayerGame goleiroTime2 { get; set; }
        public IEnumerable<PlayerGame> time1 { get; set; }
        public IEnumerable<PlayerGame> time2 { get; set; }
    }
}