using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Practica.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Practica.Data
{
    public class DbInitializer
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<PracticaUser> _userMgr;
        private PracticaContext _context;

        public DbInitializer(UserManager<PracticaUser> userMgr, RoleManager<IdentityRole> roleMgr, PracticaContext context)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _context = context;
        }

        public async Task Seed()
        {
            var user = await _userMgr.FindByNameAsync("shawnwildermuth");

            // Add User
            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await _roleMgr.CreateAsync(role);
                }

                user = new PracticaUser()
                {
                    UserName = "shawnwildermuth",
                    Email = "shawn@wildermuth.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "P@ssw0rd!");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }

            // Add Activity type
            var activityType = _context.ActivityTypes.FirstOrDefault();
            if(activityType == null)
            {
                ICollection<ActivityType> activityTypes = new List<ActivityType>();
                activityTypes.Add(
                    new ActivityType() {
                        Code = "practica",
                        Description = "practica"
                    }
                    );

                activityTypes.Add(
                    new ActivityType()
                    {
                        Code = "curs",
                        Description = "curs"
                    }
                    );

                activityTypes.Add(
                    new ActivityType()
                    {
                        Code = "eveniment",
                        Description = "eveniment"
                    }
                    );

                _context.ActivityTypes.AddRange(activityTypes);
                _context.SaveChanges();
            }
        }
    }
}
