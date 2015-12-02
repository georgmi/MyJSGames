using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyJSGames.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MyJSGames.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Game> gamesList = db.Games.OrderBy(g => g.GameTitle).ToList();
            Game newGame = new Game();
            newGame.GameTitle = "Coming Soon";
            newGame.GameAction = "ComingSoon";
            newGame.GameImage = "comingsoon.png";
            gamesList.Add(newGame);

            return View(gamesList);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles=ApplicationConstants.ADMIN)]
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult SeedGames()
        {
            List<Game> games = db.Games.ToList();
            if (games.Count == 0)
            {
                Game newGame = new Game();
                newGame.GameTitle = "Monster Wants Candy";
                newGame.GameAction = "MonsterWantsCandy";
                newGame.GameImage = "mwc.png";
                db.Games.Add(newGame);
                newGame = new Game();
                newGame.GameTitle = "Solo Pong";
                newGame.GameAction = "SoloPong";
                newGame.GameImage = "sp.png";
                db.Games.Add(newGame);
                db.SaveChanges();
            }

            //TODO: Redirect to games list when games list is implemented
            return RedirectToAction("Admin", "Home");
        }

        [Authorize(Roles = ApplicationConstants.ADMIN)]
        public ActionResult SeedUsers()
        {
            // Seed admin role
            if (!db.Roles.Any(r => r.Name.Equals(ApplicationConstants.ADMIN)))
            {
                var store = new RoleStore<IdentityRole>(db);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = ApplicationConstants.ADMIN };
                manager.Create(role);
            }

            // Seed admin user
            if (!db.Users.Any(r => r.UserName.Equals("administrator")))
            {
                var store = new UserStore<ApplicationUser>(db);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "administrator",
                    displayName = "BOFH",
                    birthDate = DateTime.Now.AddYears(-50)
                };
                manager.Create(user, "P1;assword");
                manager.AddToRole(user.Id, ApplicationConstants.ADMIN);


            }

            // Seed guest user
            if (!db.Users.Any(r => r.UserName.Equals(ApplicationConstants.GUEST)))
            {
                var store2 = new UserStore<ApplicationUser>(db);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = ApplicationConstants.GUEST,
                    displayName = ApplicationConstants.GUEST,
                    birthDate = DateTime.Now.AddYears(-21)
                };
                manager2.Create(user2, "P1;assword");
            }

            // Seed user Tony
            if (!db.Users.Any(r => r.UserName.Equals("tonybe")))
            {
                var store2 = new UserStore<ApplicationUser>(db);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = "tonybe",
                    displayName = "TBell",
                    birthDate = DateTime.Now.AddYears(-51)
                };
                manager2.Create(user2, "P1;assword");
            }

            // Seed user George
            if (!db.Users.Any(r => r.UserName.Equals("George")))
            {
                var store2 = new UserStore<ApplicationUser>(db);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = "George",
                    displayName = "Mitchell",
                    birthDate = DateTime.Now.AddYears(-45)
                };
                manager2.Create(user2, "P1;assword");
            }

            // Seed user Margaret
            if (!db.Users.Any(r => r.UserName.Equals("Margaret")))
            {
                var store2 = new UserStore<ApplicationUser>(db);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = "Margaret",
                    displayName = "emohdoubya",
                    birthDate = DateTime.Now.AddYears(-42)
                };
                manager2.Create(user2, "P1;assword");
            }

            // Seed user Liam
            if (!db.Users.Any(r => r.UserName.Equals("Liam")))
            {
                var store2 = new UserStore<ApplicationUser>(db);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = "Liam",
                    displayName = "teh_boy",
                    birthDate = DateTime.Now.AddYears(-13)
                };
                manager2.Create(user2, "P1;assword");
            }

            db.SaveChanges();

            //TODO: Redirect to user list when user list is implemented
            return RedirectToAction("Admin", "Home");
        }

        [Authorize(Roles=ApplicationConstants.ADMIN)]
        public ActionResult SeedDB()
        {
            Random rand = new Random();
            List<ApplicationUser> users = db.Users.ToList();
            List<Game> games = db.Games.ToList();
            GameScore score;
            if (users.Count > 0 && games.Count > 0)
            {
                foreach (Game game in games)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        score = new GameScore();
                        score.GameId = game.GameId;
                        score.Score = rand.Next(1, 101);
                        // BUGBUG: Randomly seeding the DB this way means some scores will be generated for Guest. 
                        // Will not fix; DBSeed is a test function.
                        score.Player = users[rand.Next(0, users.Count)];
                        score.whenPlayed = DateTime.Now.AddMinutes((rand.Next(0, 525600)) * (-1));
                        db.GameScores.Add(score);
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Leaderboards", "GameScore");
        }

        [AllowAnonymous]
        public ActionResult SignUpPrompt()
        {
            return View();
        }

    }
}