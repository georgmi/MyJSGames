using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyJSGames.Models
{
    public class ApplicationConstants
    {
        public const string ADMIN = "Admin";
        public const string GUEST = "Guest";
        public const int PAGE = 20;
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Don't show the login name; use displayName instead.
        [Display(Name = "Display Name: This is the name that other players will see in the leaderboards.")]
        public string displayName { get; set; }
        // TODO: Wish the user a happy birthday in the _LoginPartial view.
        [Display(Name = "Birthday")]
        public DateTime birthDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public System.Data.Entity.DbSet<MyJSGames.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<MyJSGames.Models.GameScore> GameScores { get; set; }
    }
}