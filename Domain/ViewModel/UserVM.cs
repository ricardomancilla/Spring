using Domain.Model;
using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class UserVM
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public RoleVM Role { get; set; }
    }
}
