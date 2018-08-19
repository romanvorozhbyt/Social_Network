using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SocialNetwork.Models;

[assembly: OwinStartup(typeof(SocialNetwork.Startup))]

namespace SocialNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultUserRoles();
        }

        private void CreateDefaultUserRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Admin rool   
                var role = new IdentityRole {Name = "Administrator"};
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    UserName = "RomanFirst",
                    Email = "romanVorozhbyt199898@gmail.com"
                };

                string userPWD = "R@v201808";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrator");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new IdentityRole {Name = "Moderator"};
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole {Name = "User"};
                roleManager.Create(role);
            }
        }
    }
}
