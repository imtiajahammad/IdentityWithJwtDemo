using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.ViewModels
{
    public class CreateRoleViewModel
    {
        public string RoleId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string RoleName { get; set; }
    }
}
