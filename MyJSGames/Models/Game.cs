using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyJSGames.Models
{
    public class Game
    {
        public virtual int GameId { get; set; }
        public virtual string GameTitle { get; set; }
        public virtual string GameAction { get; set; }
        public virtual string GameImage { get; set; }
    }
}