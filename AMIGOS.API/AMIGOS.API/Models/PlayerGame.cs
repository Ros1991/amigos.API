using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Models
{
    public class PlayerGame
    {
        public int id { get; set; }
        public int gols { get; set; }
        public int golsContra { get; set; }
        public int assistencias { get; set; }
        public bool goleiro { get; set; }
        public string name { get; set; }
    }
}