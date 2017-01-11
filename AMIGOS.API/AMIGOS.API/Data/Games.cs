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
    public static class Games
    {
        public static List<string> getAllYears()
        {
            List<string> ret = new List<string>();
            DataSet ds = AcessData.GetData("select datepart(yyyy, gameDate) as [year] from gameday group by datepart(yyyy, gameDate) order by 1 desc");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ret.Add(dr["year"].ToString());
            }
            return ret;
        }


        public static List<GameDay> getAllGamesByYear(int year)
        {
            List<GameDay> ret = new List<GameDay>();
            DataSet ds = AcessData.GetData("select * from gameday where YEAR(gamedate) = " + year + " order by gameDate desc");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GameDay gDay = new GameDay();
                gDay.id = Convert.ToInt32(dr["id"]);
                gDay.gameDate = Convert.ToDateTime(dr["gamedate"]).ToString("dd/MM/yyyy");
                List<Game> jogosFinalizados = new List<Game>();
                DataSet ds2 = AcessData.GetData("select * from game where gameday_id = " + gDay.id + " order by id");
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    Game g = new Game();
                    g.id = Convert.ToInt32(dr2["id"]);
                    g.numDeJog = Convert.ToInt32(dr2["numDeJog"]);
                    List<PlayerGame> Time1 = new List<PlayerGame>();
                    List<PlayerGame> Time2 = new List<PlayerGame>();
                    DataSet ds3 = AcessData.GetData("select goleiro, gols, golscontra, assistencias, player_id, nickname, [time] from playergame pg inner join player p on p.id = pg.player_id  where game_id = " + g.id + " order by pg.id");
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        PlayerGame pGame = new PlayerGame();
                        pGame.goleiro = bool.Parse(dr3["goleiro"].ToString());
                        pGame.gols = Convert.ToInt32(dr3["gols"]);
                        pGame.golsContra = Convert.ToInt32(dr3["golsContra"]);
                        pGame.assistencias = Convert.ToInt32(dr3["assistencias"]);
                        pGame.id = Convert.ToInt32(dr3["player_id"]);
                        pGame.name = dr3["nickName"].ToString();
                        int time = Convert.ToInt32(dr3["time"]);

                        if (pGame.goleiro && time == 1)
                        {
                            g.goleiroTime1 = pGame;
                        }
                        else if (pGame.goleiro && time == 2)
                        {
                            g.goleiroTime2 = pGame;
                        }
                        else if (time == 1)
                        {
                            Time1.Add(pGame);
                        }
                        else if (time == 2)
                        {
                            Time2.Add(pGame);
                        }
                    }
                    g.time1 = Time1;
                    g.time2 = Time2;
                    jogosFinalizados.Add(g);
                }
                gDay.jogosFinalizados = jogosFinalizados;
                ret.Add(gDay);
            }
            return ret;
        }



        public static List<GameDay> getAllGames()
        {
            List<GameDay> ret = new List<GameDay>();
            DataSet ds = AcessData.GetData("select * from gameday order by gameDate desc");

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                GameDay gDay = new GameDay();
                gDay.id = Convert.ToInt32(dr["id"]);
                gDay.gameDate = Convert.ToDateTime(dr["gamedate"]).ToString("dd/MM/yyyy");
                List<Game> jogosFinalizados = new List<Game>();
                DataSet ds2 = AcessData.GetData("select * from game where gameday_id = "+ gDay.id + " order by id");
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    Game g = new Game();
                    g.id = Convert.ToInt32(dr2["id"]);
                    g.numDeJog = Convert.ToInt32(dr2["numDeJog"]);
                    List<PlayerGame> Time1 = new List<PlayerGame>();
                    List<PlayerGame> Time2 = new List<PlayerGame>();
                    DataSet ds3 = AcessData.GetData("select goleiro, gols, golscontra, assistencias, player_id, nickname, [time] from playergame pg inner join player p on p.id = pg.player_id  where game_id = " + g.id + " order by pg.id");
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        PlayerGame pGame = new PlayerGame();
                        pGame.goleiro = bool.Parse(dr3["goleiro"].ToString());
                        pGame.gols = Convert.ToInt32(dr3["gols"]);
                        pGame.golsContra = Convert.ToInt32(dr3["golsContra"]);
                        pGame.assistencias = Convert.ToInt32(dr3["assistencias"]);
                        pGame.id = Convert.ToInt32(dr3["player_id"]);
                        pGame.name = dr3["nickName"].ToString();
                        int time = Convert.ToInt32(dr3["time"]);

                        if (pGame.goleiro && time == 1)
                        {
                            g.goleiroTime1 = pGame;
                        }
                        else if (pGame.goleiro && time == 2)
                        {
                            g.goleiroTime2 = pGame;
                        }
                        else if (time == 1)
                        {
                            Time1.Add(pGame);
                        }
                        else if (time == 2)
                        {
                            Time2.Add(pGame);
                        }
                    }
                    g.time1 = Time1;
                    g.time2 = Time2;
                    jogosFinalizados.Add(g);
                }
                gDay.jogosFinalizados = jogosFinalizados;
                ret.Add(gDay);
            }
            return ret;
        }

        //public static Player getPlayerById(string URL, int id)
        //{
        //    DataSet ds = AcessData.GetData("select * from Player where id = " + id);
        //    Player plr = new Player();
        //    DataRow dr = ds.Tables[0].Rows[0];
        //    plr.Id = Convert.ToInt32(dr["id"]);
        //    if (!(dr["Birthday"] is System.DBNull)) plr.BirthdayDate = Convert.ToDateTime(dr["Birthday"]);
        //    if (!(dr["Birthday"] is System.DBNull)) plr.Birthday = plr.BirthdayDate.ToString("dd/MM/yyyy");
        //    plr.Name = dr["Name"].ToString();
        //    plr.NickName = dr["Nickname"].ToString();
        //    plr.Email = dr["Email"].ToString();
        //    plr.Phone = dr["Phone"].ToString();
        //    plr.Position = dr["Position"].ToString();
        //    plr.Picture = dr["Picture"].ToString();
        //    if (!(dr["Subscriber"] is System.DBNull)) plr.Subscriber = bool.Parse(dr["Subscriber"].ToString());
        //    return plr;
        //}

        //public static int deletePlayer(int id)
        //{
        //    string commandString = "delete from [dbo].[Player] where id = " + id.ToString();
        //    return AcessData.ExecuteCommand(commandString);
        //}

        //public static int updatePlayer(int id, Player plr, string URL)
        //{
        //    DateTime dt = new DateTime();
        //    if (string.IsNullOrEmpty(plr.Picture))
        //    {
        //        plr.Picture = "assets/images/pic.png";
        //    }
        //    else
        //    {
        //        plr.Picture = URL + plr.Picture;
        //    }

        //    string commandString = "UPDATE [dbo].[Player] set ";
        //    commandString += "[Nickname] = '" + plr.NickName + "'";
        //    commandString += ",[Birthday] = " + (!string.IsNullOrEmpty(plr.Birthday) && DateTime.TryParse(plr.Birthday, out dt)? "'" + dt.ToString("yyyy-MM-dd") + "'" : "null");
        //    commandString += ",[Name] = " + (!string.IsNullOrEmpty(plr.Name) ? "'" + plr.Name + "'" : "null");
        //    commandString += ",[Email] = " + (!string.IsNullOrEmpty(plr.Email) ? "'" + plr.Email + "'" : "null");
        //    commandString += ",[Phone] = " + (!string.IsNullOrEmpty(plr.Phone) ? "'" + plr.Phone + "'" : "null");
        //    commandString += ",[Position] = " + (!string.IsNullOrEmpty(plr.Position) ? "'" + plr.Position + "'" : "null");
        //    commandString += ",[Picture] = " + (!string.IsNullOrEmpty(plr.Picture) ? "'" + plr.Picture + "'" : "null");
        //    commandString += ",[Subscriber] = " + Convert.ToInt16(plr.Subscriber).ToString();



        //    commandString += " where id = " + id.ToString();
        //    return AcessData.ExecuteCommand(commandString);
        //}

        public static int insertGameDay(GameDay gDay)
        {
            CultureInfo ptBR = new CultureInfo("pt-BR");
            DateTime gDayDate = new DateTime();
            if (DateTime.TryParse(gDay.gameDate, ptBR, DateTimeStyles.None, out gDayDate))
            {
                string commandString = "INSERT INTO [dbo].[GameDay] ([gameDate]) OUTPUT INSERTED.id VALUES ('" + gDayDate.ToString("yyyy-MM-dd") + "')";
                gDay.id = AcessData.ExecuteCommand(commandString);
                foreach(Game g in gDay.jogosFinalizados)
                {
                    insertGame(g, gDay.id);
                }
                return gDay.id;
            }
            return 0;
        }

        public static int insertGame(Game g, int parentID)
        {
            string commandString = "INSERT INTO [dbo].[game] ([numDeJog], [GameDay_id]) OUTPUT INSERTED.id VALUES (" + g.numDeJog + ", " + parentID + ")";
            g.id = AcessData.ExecuteCommand(commandString);
            if (g.goleiroTime1 != null)
            {
                insertGamePlayer(g.goleiroTime1, g.id, 1);
            }
            if (g.goleiroTime2 != null)
            {
                insertGamePlayer(g.goleiroTime2, g.id, 2);
            }
            foreach (PlayerGame pg in g.time1)
            {
                insertGamePlayer(pg, g.id, 1);
            }
            foreach (PlayerGame pg in g.time2)
            {
                insertGamePlayer(pg, g.id, 2);
            }
            return 0;
        }

        public static int insertGamePlayer(PlayerGame pGame, int parentID, int time)
        {
            string commandString = "INSERT INTO [dbo].[playergame] ([gols],[golsContra],[assistencias],[goleiro],[time],[game_id], [player_id])" +
                "VALUES (" + pGame.gols + "," + pGame.golsContra+ "," + pGame.assistencias + "," + Convert.ToInt32(pGame.goleiro) + "," + time + "," + parentID + "," +pGame.id+ ")";

            return AcessData.ExecuteCommand(commandString);
        }

    }
}