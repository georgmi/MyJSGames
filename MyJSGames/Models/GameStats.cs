using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MyJSGames.Models
{
    public class LeaderboardsViewModel
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public List<Leaderboard> Leaderboards { get; set; }
    }

    public class Leaderboard
    {
        public Game game { get; set; }
        public int Offset { get; set; }
        public List<GameScore> ScoreList { get; set; }

        public Leaderboard(Game gameInput, List<GameScore> scoreList)
        {
            game = gameInput;
            ScoreList = scoreList;
            Offset = 0;
        }
    }

    public class GameSummary
    {
        public Game game { get; set; }
        //public List<GameScore> ScoreList { get; set; }
        public int gamesPlayed { get; set; }
        public int maxScore { get; set; }
        public int minScore { get; set; }
        [DisplayFormat(DataFormatString="{0:f3}")]
        public double avgScore { get; set; }

        public GameSummary(Game gameInput, List<GameScore> scoreList)
        {
            game = gameInput;
            //ScoreList = scoreList;
            gamesPlayed = scoreList.Count;
            if (scoreList.Count > 0)
            {
                maxScore = scoreList.Max(score => score.Score);
                minScore = scoreList.Min(score => score.Score);
                avgScore = scoreList.Average(s => s.Score);
            }
            else
            {
                maxScore = 0;
                minScore = 0;
                avgScore = 0;
            }
        }
    }

    public class PlayerGamePair
    {
        public Game Game { get; set; }
        public ApplicationUser User { get; set; }

        public PlayerGamePair(Game game, ApplicationUser user)
        {
            Game = game;
            User = user;
        }
    }
}