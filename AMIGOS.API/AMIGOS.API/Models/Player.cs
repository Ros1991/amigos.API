using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMIGOS.API.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Birthday { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Picture { get; set; }
        public bool Subscriber  { get; set; }
    }
}