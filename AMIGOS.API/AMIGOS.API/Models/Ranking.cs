using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Models
{
    public class RankingPlayer
    {
        public int player_id { get; set; }
        public string Peladeiro { get; set; }
        public int Pontos { get; set; }
        public double MediaPontos { get; set; }
        public int Gp { get; set; }
        public int ast { get; set; }
        public double MediaGols { get; set; }
        public int jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int gc { get; set; }
        public int dias { get; set; }
        public bool ranking { get; set; }
    }
}