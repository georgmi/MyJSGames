using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Allows for customizaion of display names and editor controls, among other things.
using System.Linq;
using System.Web;

namespace MyJSGames.Models
{
    public class GameScore
    {
        public virtual int GameScoreId { get; set; }
        public virtual int GameId { get; set; }
        [Display(Name = "Player")] // Sets display name
        //public virtual string UserName { get; set; }
        public virtual ApplicationUser Player { get; set; }
        public virtual int Score { get; set; }
        [Display(Name = "Date Played")]
        [DataType(DataType.DateTime)] // Sets editor
        public virtual DateTime whenPlayed { get; set; }
    }
}