using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyJSGames.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MyJSGames
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(
            //    new DropCreateDatabaseAlways<Models.ApplicationDbContext>());
            //Database.SetInitializer(
            //    new DropCreateDatabaseAlways<IdentityDbContext>());

            SetUpDB();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetUpDB()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.Games.ToList().Count == 0)
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
            }

            using (ApplicationDbContext identity = new ApplicationDbContext())
            {
                // Seed admin role
                if (!identity.Roles.Any(r => r.Name.Equals(ApplicationConstants.ADMIN)))
                {
                    var store = new RoleStore<IdentityRole>(identity);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = ApplicationConstants.ADMIN };
                    manager.Create(role);
                }

                // Seed admin user
                if (!identity.Users.Any(r => r.UserName.Equals("administrator")))
                {
                    var store = new UserStore<ApplicationUser>(identity);
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
                if (!identity.Users.Any(r => r.UserName.Equals(ApplicationConstants.GUEST)))
                {
                    var store2 = new UserStore<ApplicationUser>(identity);
                    var manager2 = new UserManager<ApplicationUser>(store2);
                    var user2 = new ApplicationUser
                    {
                        UserName = ApplicationConstants.GUEST,
                        displayName = ApplicationConstants.GUEST,
                        birthDate = DateTime.Now.AddYears(-21)
                    };
                    manager2.Create(user2, "P1;assword");
                }
            }
        }
    }
}
