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
            var user = await _userMgr.FindByNameAsync("testadmin");

            // Add Users
            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await _roleMgr.CreateAsync(role);
                }
                if (!(await _roleMgr.RoleExistsAsync("Student")))
                {
                    var role = new IdentityRole("Student");
                    await _roleMgr.CreateAsync(role);
                }
                if (!(await _roleMgr.RoleExistsAsync("Teacher")))
                {
                    var role = new IdentityRole("Teacher");
                    await _roleMgr.CreateAsync(role);
                }
                if (!(await _roleMgr.RoleExistsAsync("Company")))
                {
                    var role = new IdentityRole("Company");
                    await _roleMgr.CreateAsync(role);
                }

                // Add admin
                user = new PracticaUser()
                {
                    UserName = "testadmin",
                    Email = "testadmin@yahoo.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "Pp123456#");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                //var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));
                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build admin users and role");
                }

                // Add student
                user = new PracticaUser()
                {
                    UserName = "teststudent",
                    Email = "teststudent@yahoo.com"
                };
                userResult = await _userMgr.CreateAsync(user, "Pp123456#");
                roleResult = await _userMgr.AddToRoleAsync(user, "Student");
                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build student users and role");
                }

                // Add teacher
                user = new PracticaUser()
                {
                    UserName = "testteacher",
                    Email = "testteacher@yahoo.com"
                };
                userResult = await _userMgr.CreateAsync(user, "Pp123456#");
                roleResult = await _userMgr.AddToRoleAsync(user, "Teacher");
                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build teacher users and role");
                }

                // Add company
                user = new PracticaUser()
                {
                    UserName = "testcompany",
                    Email = "testcompany@yahoo.com"
                };
                userResult = await _userMgr.CreateAsync(user, "Pp123456#");
                roleResult = await _userMgr.AddToRoleAsync(user, "Company");
                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build company users and role");
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
