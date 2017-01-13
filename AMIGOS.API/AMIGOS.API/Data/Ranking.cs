using AMIGOS.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Data
{
    public static class Ranking
    {
        public static List<RankingPlayer> getBySemester()
        {
            List<RankingPlayer> ret = new List<RankingPlayer>();
            DataSet ds = AcessData.GetData("select *, case when dias > ((select * from gamesInsemester) * 0.25) then 1 else 0 end as ranking from rankingByCurrentSemester()");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                RankingPlayer r = new RankingPlayer();
                r.player_id = Convert.ToInt32(dr["player_id"]);
                r.Peladeiro = dr["Peladeiro"].ToString();
                r.Pontos = Convert.ToInt32(dr["Pontos"]);
                r.MediaPontos = Convert.ToDouble(dr["MediaPontos"]);
                r.Gp = Convert.ToInt32(dr["Gp"]);
                r.ast = Convert.ToInt32(dr["ast"]);
                r.MediaGols = Convert.ToDouble(dr["MediaGols"]);
                r.jogos = Convert.ToInt32(dr["jogos"]);
                r.Vitorias = Convert.ToInt32(dr["Vitorias"]);
                r.Empates = Convert.ToInt32(dr["Empates"]);
                r.Derrotas = Convert.ToInt32(dr["Derrotas"]);
                r.gc = Convert.ToInt32(dr["gc"]);
                r.dias = Convert.ToInt32(dr["dias"]);
                r.ranking = Convert.ToBoolean(dr["ranking"]);
                ret.Add(r);
            }
            DataSet dsg = AcessData.GetData("select *, case when dias > ((select * from gamesInsemester) * 0.25) then 1 else 0 end as ranking from rankingByCurrentSemesterGoalkeeper()");
            foreach (DataRow dr in dsg.Tables[0].Rows)
            {
                RankingPlayer r = new RankingPlayer();
                r.player_id = Convert.ToInt32(dr["player_id"]);
                r.Peladeiro = dr["Peladeiro"].ToString();
                r.Pontos = Convert.ToInt32(dr["Pontos"]);
                r.MediaPontos = Convert.ToDouble(dr["MediaPontos"]);
                r.MediaGols = Convert.ToDouble(dr["MediaGols"]);
                r.jogos = Convert.ToInt32(dr["jogos"]);
                r.Vitorias = Convert.ToInt32(dr["Vitorias"]);
                r.Empates = Convert.ToInt32(dr["Empates"]);
                r.Derrotas = Convert.ToInt32(dr["Derrotas"]);
                r.gc = Convert.ToInt32(dr["golssofridos"]);
                r.dias = Convert.ToInt32(dr["dias"]);
                r.ranking = Convert.ToBoolean(dr["ranking"]);
                r.goleiro = true;
                ret.Add(r);
            }

            return ret;
        }


        public static List<RankingPlayer> getByYear(int year)
        {
            List<RankingPlayer> ret = new List<RankingPlayer>();
            DataSet ds = AcessData.GetData("select *, case when dias > ((select gamesinyear from gamesInyear where [year] = "+ year + ") * 0.25) then 1 else 0 end as ranking from rankingByYear(" + year + ")");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                RankingPlayer r = new RankingPlayer();
                r.player_id = Convert.ToInt32(dr["player_id"]);
                r.Peladeiro = dr["Peladeiro"].ToString();
                r.Pontos = Convert.ToInt32(dr["Pontos"]);
                r.MediaPontos = Convert.ToDouble(dr["MediaPontos"]);
                r.Gp = Convert.ToInt32(dr["Gp"]);
                r.ast = Convert.ToInt32(dr["ast"]);
                r.MediaGols = Convert.ToDouble(dr["MediaGols"]);
                r.jogos = Convert.ToInt32(dr["jogos"]);
                r.Vitorias = Convert.ToInt32(dr["Vitorias"]);
                r.Empates = Convert.ToInt32(dr["Empates"]);
                r.Derrotas = Convert.ToInt32(dr["Derrotas"]);
                r.gc = Convert.ToInt32(dr["gc"]);
                r.dias = Convert.ToInt32(dr["dias"]);
                r.ranking = Convert.ToBoolean(dr["ranking"]);
                ret.Add(r);
            }

            DataSet dsg = AcessData.GetData("select *, case when dias > ((select gamesinyear from gamesInyear where [year] = " + year + ") * 0.25) then 1 else 0 end as ranking from rankingByYearGoalkeeper(" + year + ")");
            foreach (DataRow dr in dsg.Tables[0].Rows)
            {
                RankingPlayer r = new RankingPlayer();
                r.player_id = Convert.ToInt32(dr["player_id"]);
                r.Peladeiro = dr["Peladeiro"].ToString();
                r.Pontos = Convert.ToInt32(dr["Pontos"]);
                r.MediaPontos = Convert.ToDouble(dr["MediaPontos"]);
                r.MediaGols = Convert.ToDouble(dr["MediaGols"]);
                r.jogos = Convert.ToInt32(dr["jogos"]);
                r.Vitorias = Convert.ToInt32(dr["Vitorias"]);
                r.Empates = Convert.ToInt32(dr["Empates"]);
                r.Derrotas = Convert.ToInt32(dr["Derrotas"]);
                r.gc = Convert.ToInt32(dr["golssofridos"]);
                r.dias = Convert.ToInt32(dr["dias"]);
                r.ranking = Convert.ToBoolean(dr["ranking"]);
                r.goleiro = true;
                ret.Add(r);
            }
            return ret;
        }
    }
}