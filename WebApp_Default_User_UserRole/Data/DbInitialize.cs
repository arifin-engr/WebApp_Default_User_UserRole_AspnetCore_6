using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp_Default_User_UserRole.Models;

namespace WebApp_Default_User_UserRole.Data
{
    public class DbInitialize
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitialize(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            //create default role

            string _defultRole = "Super Admin";
            _defultRole.Trim();
            var _roleExist=_roleManager.RoleExistsAsync(_defultRole).Result;
            if (!_roleExist)
            {
                _roleManager.CreateAsync(new IdentityRole(_defultRole)).GetAwaiter().GetResult();
            }

            //create default user
            string _userName = "arifin20";
            string _email = "arifin123@gmail.com";
            string _password = "112233";

            _userName.Trim();
            _email.Trim();
            _password.Trim();

            var userNameExist= _context.Users.FirstOrDefaultAsync(x => x.UserName == _userName).Result;
            if (userNameExist==null)
            {
                AppUser appUser = new AppUser();
                appUser.UserName = _userName;
                appUser.Email = _email;

                IdentityResult UserCreateresult = _userManager.CreateAsync(appUser, _password).Result;

                //assign default role
                if (UserCreateresult.Succeeded)
                {
                    IdentityResult assignResult = _userManager.AddToRoleAsync(appUser, _defultRole).Result;
                    if (assignResult.Succeeded)
                    {
                        _context.SaveChanges();
                        // u can add any message here
                    }
                }

            }


            
        }
    }
}
