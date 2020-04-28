namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        RoleDescription = c.String(nullable: false, maxLength: 200),
                        CreateDtm = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreateUsr = c.String(),
                        UpdateDtm = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdateUsr = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreateDtm = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreateUsr = c.String(),
                        UpdateDtm = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdateUsr = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 80),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        CreateDtm = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreateUsr = c.String(),
                        UpdateDtm = c.DateTime(precision: 7, storeType: "datetime2"),
                        UpdateUsr = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRole");
            DropTable("dbo.Roles");
        }
    }
}
