namespace ToDoList_Jefremov_.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDoList_Jefremov_.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoList_Jefremov_.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoList_Jefremov_.Models.ApplicationDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			AddUsers(context);
		}

		void AddUsers(ToDoList_Jefremov_.Models.ApplicationDbContext context)
		{
			var user = new ApplicationUser { UserName = "user1@gmail.com" };
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));
			um.Create(user, "password");
		}
	}
}