namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppContext context)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                context.UserRol.Add(new Domain.Model.UserRole()
                {
                    User = new Domain.Model.User()
                    {
                        Username = "Luis",
                        Password = "bc974680bb94583b3836a4378a600016", //Qwerty01
                        Email = "luisricardomancilla@gmail.com",
                        PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("bc974680bb94583b3836a4378a600016")),
                        PasswordSalt = hmac.Key,
                        CreateUsr = "Luis"
                    },
                    Role = new Domain.Model.Role()
                    {
                        RoleName = "Administrator",
                        RoleDescription = "Administrator",
                        CreateUsr = "Luis"
                    },
                    CreateUsr = "Luis"
                });

                context.Roles.Add(new Domain.Model.Role()
                {
                    RoleName = "User",
                    RoleDescription = "User",
                    CreateUsr = "Luis"
                });
            }
        }
    }
}
