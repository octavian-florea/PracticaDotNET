using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Practica.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Data
{
    public class DbInitializer
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<PracticaUser> _userMgr;
        


        public DbInitializer(UserManager<PracticaUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
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
        }
    }
}
