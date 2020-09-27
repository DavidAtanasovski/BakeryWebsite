using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<IdentityUser>();
        }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}
