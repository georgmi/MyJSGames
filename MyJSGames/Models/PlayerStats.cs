using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyJSGames.Models;

namespace MyJSGames.Models
{
    public class PlayerStats
    {
        public ApplicationUser Player { get; set; }
        public List<GameSummary> GameSummaries { get; set; }
        public string AdminRoleID { get; set; }

        public PlayerStats(ApplicationUser player, List<GameSummary> gameSummaries)
        {
            Player = player;
            GameSummaries = gameSummaries;
        }
    }
}