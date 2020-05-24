using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace service_auto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class UsersAPIController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersAPIController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/UsersAPI
        [HttpGet]
        public async Task<string> Get()
        {
            var users = _userManager.Users.ToList();
            List<IdentityUser> employees = new List<IdentityUser>();

            foreach (IdentityUser user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Employee"))
                    employees.Add(user);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(employees, options);

        }


    }
}
