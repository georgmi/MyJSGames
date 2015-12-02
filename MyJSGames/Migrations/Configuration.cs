namespace MyJSGames.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyJSGames.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MyJSGames.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyJSGames.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Seed admin role
            if (!context.Roles.Any(r => r.Name.Equals(ApplicationConstants.ADMIN)))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = ApplicationConstants.ADMIN };
                manager.Create(role);
            }


            // Seed admin user
            if (!context.Users.Any(r => r.UserName.Equals("administrator")))
            {
                var store = new UserStore<ApplicationUser>(context);
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
            if (!context.Users.Any(r => r.UserName.Equals(ApplicationConstants.GUEST)))
            {
                var store2 = new UserStore<ApplicationUser>(context);
                var manager2 = new UserManager<ApplicationUser>(store2);
                var user2 = new ApplicationUser
                {
                    UserName = ApplicationConstants.GUEST,
                    displayName = ApplicationConstants.GUEST,
                    birthDate = DateTime.Now.AddYears(-21)
                };
                manager2.Create(user2, "P1;assword");
            }

            if (!context.Games.Any(g => g.GameTitle.Equals("Monster Wants Candy")))
            {
                Game newGame = new Game();
                newGame.GameTitle = "Monster Wants Candy";
                newGame.GameAction = "MonsterWantsCandy";
                newGame.GameImage = "mwc.png";
                context.Games.Add(newGame);
            }

            if (!context.Games.Any(g => g.GameTitle.Equals("Solo Pong")))
            {
                Game newGame = new Game();
                newGame.GameTitle = "Solo Pong";
                newGame.GameAction = "SoloPong";
                newGame.GameImage = "sp.png";
                context.Games.Add(newGame);
            }

            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
