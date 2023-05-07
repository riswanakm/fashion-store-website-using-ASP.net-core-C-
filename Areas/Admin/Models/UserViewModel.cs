using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Group7_FinalProject.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
