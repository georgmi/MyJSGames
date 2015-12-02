using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyJSGames.Models
{
    public class ProfileStats
    {
        public string PlayerName;
        public List<GameScore> ScoreList;

        public ProfileStats(string player, List<GameScore> scores)
        {
            PlayerName = player;
            ScoreList = scores;
        }
    }
}