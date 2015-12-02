using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyJSGames.Models;
using PagedList;

namespace MyJSGames.Controllers
{
    public class GameScoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Random rand = new Random();

        // GET: /GameScore/
        [Authorize(Roles=ApplicationConstants.ADMIN)]
        public ActionResult Index()
        {
            return RedirectToAction("Leaderboards");
        }

        // GET: /GameScore/Details/5
        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameScore gamescore = db.GameScores.Find(id);
            if (gamescore == null)
            {
                return HttpNotFound();
            }
            return View(gamescore);
        }

        // GET: /GameScore/Delete/5
        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameScore gamescore = db.GameScores.Find(id);
            if (gamescore == null)
            {
                return HttpNotFound();
            }
            return View(gamescore);
        }

        // POST: /GameScore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult DeleteConfirmed(int id)
        {
            GameScore gamescore = db.GameScores.Find(id);
            db.GameScores.Remove(gamescore);
            db.SaveChanges();
            return RedirectToAction("Leaderboards");
        }

        // GET: /GameScore/ClearLeaderBoard
        [Authorize(Roles=ApplicationConstants.ADMIN)]
        public ActionResult ClearLeaderBoard(Game game)
        {
            List<GameScore> scores = db.GameScores.Where(s => s.GameId == game.GameId).ToList();
            foreach (GameScore score in scores)
            {
                db.GameScores.Remove(score);
            }
            db.SaveChanges();

            return RedirectToAction("Leaderboards", "GameScore");
        }

        // GET: /GameScore/ClearAllLeaderBoards
        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult ClearAllLeaderBoards()
        {
            List<GameScore> scores = db.GameScores.ToList();
            foreach (GameScore score in scores)
            {
                db.GameScores.Remove(score);
            }
            db.SaveChanges();

            return RedirectToAction("Leaderboards", "GameScore");
        }

        //GET: /GameScore/ClearUserScores
        [Authorize]
        public ActionResult ClearUserScores(Game game)
        {
            List<GameScore> scores = db.GameScores.Where(s => s.GameId == game.GameId).Where(s => s.Player.UserName == User.Identity.Name).ToList();
            foreach (GameScore score in scores)
            {
                db.GameScores.Remove(score);
            }
            db.SaveChanges();

            return RedirectToAction("MyProfile", "Account");
        }

        // GET: /GameScore/AdminClearScoresGameAndUser
        public ActionResult AdminClearScoresGameAndUser(PlayerGamePair pair)
        {
            List<GameScore> scores = db.GameScores.Where(s => s.Player.UserName == pair.User.UserName).Where(s => s.GameId == pair.Game.GameId).ToList();
            foreach (GameScore score in scores)
            {
                db.GameScores.Remove(score);
            }
            db.SaveChanges();
            return RedirectToAction("UserList", "Account");
        }

        // GET: /GameScore/AdminClearAllUserScores
        public ActionResult AdminClearAllUserScores(ApplicationUser user)
        {
            List<GameScore> scores = db.GameScores.Where(s => s.Player.UserName == user.UserName).ToList();
            foreach (GameScore score in scores)
            {
                db.GameScores.Remove(score);
            }
            db.SaveChanges();
            return RedirectToAction("UserList", "Account");
        }

        // GET: /GameScore/Leaderboards
        public ActionResult Leaderboards()
        {
            List<Game> gameList = db.Games.OrderBy(g => g.GameTitle).ToList();
            List<GameScore> scoreList = db.GameScores.ToList();
            List<GameScore> tempScoreList = new List<GameScore>();
            List<Leaderboard> leaderboards = new List<Leaderboard>();
            foreach(Game g in gameList)
            {
                tempScoreList = scoreList.Where(gs => gs.GameId.Equals(g.GameId)).OrderByDescending(gs => gs.Score).ToList();
                leaderboards.Add(new Leaderboard(g, tempScoreList));
            }
            return View(leaderboards);
        }

        // GET: /GameScore/Leaderboard/5
        public ActionResult Leaderboard(int? id, int? pageOffset)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            List<GameScore> scoreList = db.GameScores.Where(gs => gs.GameId.Equals(game.GameId)).OrderByDescending(gs => gs.Score).ToList();
            Leaderboard leaderboard = new Leaderboard(game, scoreList);
            if (pageOffset != null && pageOffset > 0 && ((pageOffset) < scoreList.Count )) //Defend against out-of-range offsets on the GET.
            {
                leaderboard.Offset = (int)pageOffset;
            }
            return View(leaderboard);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
