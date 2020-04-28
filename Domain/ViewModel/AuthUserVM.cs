﻿using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class AuthUserVM
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public RoleVM Role { get; set; }
    }
}
