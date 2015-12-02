using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyJSGames.Models;

namespace MyJSGames.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /Games/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Games/MonsterWantsCandy
        [AllowAnonymous]
        public ActionResult MonsterWantsCandy()
        {
            return View();
        }

        //
        // GET: /Games/SoloPong
        [AllowAnonymous]
        public ActionResult SoloPong()
        {
            return View();
        }

        //
        // GET: /Games/SoloPong
        [AllowAnonymous]
        public ActionResult ComingSoon()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult GameOver(string gameName, int score, string playerName, string whenScored)
        {
            // DONE: Check authentication status
            // DONE: Restrict GameScore creation to only authenticated users.
            if (User.Identity.IsAuthenticated)
            {
                Game game = db.Games.Where(g => (g.GameTitle == gameName)).First();
                // ApplicationUser appUser = (ApplicationUser)HttpContext.Session[User.Identity.Name];
                if (game != null)
                {
                    if (playerName == "" || playerName == null)
                    {
                        playerName = "Guest";
                    }
                    GameScore newScore = new GameScore();
                    newScore.GameId = game.GameId;
                    newScore.Score = score;
                    newScore.Player = db.Users.Where(u => u.UserName == User.Identity.Name).First();
                    newScore.whenPlayed = DateTime.Now;

                    db.GameScores.Add(newScore);
                    db.SaveChanges();
                }

                return RedirectToAction("Leaderboards", "GameScore");
                //return playerName + " played " + gameName + " on " + whenScored + " and scored " + score.ToString();
            }
            // DONE: Redirect anonymous users to request to create account or log in.
            return RedirectToAction("SignUpPrompt", "Home");
        }
    }
}