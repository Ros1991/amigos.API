using AMIGOS.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Data
{
    public static class Players
    {
        
        public static List<Player> getAllPlayers(string url)
        {
            List<Player> ret = new List<Models.Player>();
            DataSet ds = AcessData.GetData("select * from Player");

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Player plr = new Player();
                plr.Id = Convert.ToInt32(dr["id"]);
                if(!(dr["Birthday"] is System.DBNull)) plr.BirthdayDate = Convert.ToDateTime(dr["Birthday"]);
                if (!(dr["Birthday"] is System.DBNull)) plr.Birthday = plr.BirthdayDate.ToString("dd/MM/yyyy");
                plr.Name = dr["Name"].ToString();
                plr.NickName = dr["Nickname"].ToString();
                plr.Email = dr["Email"].ToString();
                plr.Phone = dr["Phone"].ToString();
                plr.Position = dr["Position"].ToString();
                plr.Picture = dr["Picture"].ToString();
                if (!(dr["Subscriber"] is System.DBNull)) plr.Subscriber = bool.Parse(dr["Subscriber"].ToString());

                ret.Add(plr);
            }
            return ret;
        }

        public static Player getPlayerById(string URL, int id)
        {
            DataSet ds = AcessData.GetData("select * from Player where id = " + id);
            Player plr = new Player();
            DataRow dr = ds.Tables[0].Rows[0];
            plr.Id = Convert.ToInt32(dr["id"]);
            if (!(dr["Birthday"] is System.DBNull)) plr.BirthdayDate = Convert.ToDateTime(dr["Birthday"]);
            if (!(dr["Birthday"] is System.DBNull)) plr.Birthday = plr.BirthdayDate.ToString("dd/MM/yyyy");
            plr.Name = dr["Name"].ToString();
            plr.NickName = dr["Nickname"].ToString();
            plr.Email = dr["Email"].ToString();
            plr.Phone = dr["Phone"].ToString();
            plr.Position = dr["Position"].ToString();
            plr.Picture = dr["Picture"].ToString();
            if (!(dr["Subscriber"] is System.DBNull)) plr.Subscriber = bool.Parse(dr["Subscriber"].ToString());
            return plr;
        }

        public static int deletePlayer(int id)
        {
            string commandString = "delete from [dbo].[Player] where id = " + id.ToString();
            return AcessData.ExecuteCommand(commandString);
        }

        public static int updatePlayer(int id, Player plr, string URL)
        {
            DateTime dt = new DateTime();
            if (string.IsNullOrEmpty(plr.Picture))
            {
                plr.Picture = "assets/images/pic.png";
            }
            else
            {
                plr.Picture = URL + plr.Picture;
            }

            string commandString = "UPDATE [dbo].[Player] set ";
            commandString += "[Nickname] = '" + plr.NickName + "'";
            commandString += ",[Birthday] = " + (!string.IsNullOrEmpty(plr.Birthday) && DateTime.TryParse(plr.Birthday, out dt)? "'" + dt.ToString("yyyy-MM-dd") + "'" : "null");
            commandString += ",[Name] = " + (!string.IsNullOrEmpty(plr.Name) ? "'" + plr.Name + "'" : "null");
            commandString += ",[Email] = " + (!string.IsNullOrEmpty(plr.Email) ? "'" + plr.Email + "'" : "null");
            commandString += ",[Phone] = " + (!string.IsNullOrEmpty(plr.Phone) ? "'" + plr.Phone + "'" : "null");
            commandString += ",[Position] = " + (!string.IsNullOrEmpty(plr.Position) ? "'" + plr.Position + "'" : "null");
            commandString += ",[Picture] = " + (!string.IsNullOrEmpty(plr.Picture) ? "'" + plr.Picture + "'" : "null");
            commandString += ",[Subscriber] = " + Convert.ToInt16(plr.Subscriber).ToString();



            commandString += " where id = " + id.ToString();
            return AcessData.ExecuteCommand(commandString);
        }

        public static int insertPlayer(Player plr, string URL)
        {
            string commandString = "";

            commandString += "INSERT INTO[dbo].[Player]([Nickname]";
            if (!string.IsNullOrEmpty(plr.Birthday)) commandString += ",[Birthday]";
            if (!string.IsNullOrEmpty(plr.Name)) commandString += ",[Name]";
            if (!string.IsNullOrEmpty(plr.Email)) commandString += ",[Email]";
            if (!string.IsNullOrEmpty(plr.Phone)) commandString += ",[Phone]";
            if (!string.IsNullOrEmpty(plr.Position)) commandString += ",[Position]";
            if (!string.IsNullOrEmpty(plr.Picture)) commandString += ",[Picture]";
            if (plr.Subscriber) commandString += ",[Subscriber]";

            commandString += ") VALUES ('"+plr.NickName+"'";

            if (string.IsNullOrEmpty(plr.Picture))
            {
                plr.Picture = "assets/images/pic.png";
            }
            else
            {
                plr.Picture = URL + plr.Picture;
            }

            if (!string.IsNullOrEmpty(plr.Birthday)) commandString += ",'" + DateTime.Parse(plr.Birthday).ToString("yyyy-MM-dd") +"'";
            if (!string.IsNullOrEmpty(plr.Name)) commandString += ",'" + plr.Name + "'";
            if (!string.IsNullOrEmpty(plr.Email)) commandString += ",'" + plr.Email + "'";
            if (!string.IsNullOrEmpty(plr.Phone)) commandString += ",'" + plr.Phone + "'";
            if (!string.IsNullOrEmpty(plr.Position)) commandString += ",'" + plr.Position + "'";
            if (!string.IsNullOrEmpty(plr.Picture)) commandString += ",'" + plr.Picture + "'";
            if (plr.Subscriber) commandString += "," + Convert.ToInt16(plr.Subscriber).ToString();

            commandString += ")";

            return AcessData.ExecuteCommand(commandString);
        }
    }
}