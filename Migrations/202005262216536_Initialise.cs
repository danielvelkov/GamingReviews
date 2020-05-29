namespace GamingReviews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Entity_Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        User_id = c.Int(nullable: false),
                        Game_id = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Header = c.String(nullable: false),
                        Image = c.Binary(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Entity_Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_id, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.Entity_Id)
                .Index(t => t.Entity_Id)
                .Index(t => t.User_id)
                .Index(t => t.Game_id);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        Entity_Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Entity_Id)
                .Index(t => t.Entity_Id, unique: true);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Entity_Id = c.Int(nullable: false),
                        TargetEntity_Id = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Entity_Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.Entity_Id)
                .ForeignKey("dbo.Entities", t => t.TargetEntity_Id, cascadeDelete: true)
                .Index(t => t.Entity_Id)
                .Index(t => t.TargetEntity_Id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 255),
                        UserType = c.Int(nullable: false),
                        Password = c.String(maxLength: 10),
                        Image = c.Binary(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Entity_id = c.Int(nullable: false),
                        Summary = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 255),
                        User_id = c.Int(nullable: false),
                        Image = c.Binary(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Entity_id)
                .ForeignKey("dbo.Entities", t => t.Entity_id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: false)
                .Index(t => t.Entity_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Entity_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Content = c.String(nullable: false),
                        Game_id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Entity_id)
                .ForeignKey("dbo.Entities", t => t.Entity_id)
                .ForeignKey("dbo.Games", t => t.Game_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Entity_id)
                .Index(t => t.User_id)
                .Index(t => t.Game_id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Activity = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Entity_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                        Reaction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Entity_id, t.User_id })
                .ForeignKey("dbo.Entities", t => t.Entity_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Entity_id)
                .Index(t => t.User_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Entity_Id", "dbo.Entities");
            DropForeignKey("dbo.Comments", "TargetEntity_Id", "dbo.Entities");
            DropForeignKey("dbo.Comments", "Entity_Id", "dbo.Entities");
            DropForeignKey("dbo.Votes", "User_id", "dbo.Users");
            DropForeignKey("dbo.Votes", "Entity_id", "dbo.Entities");
            DropForeignKey("dbo.Logs", "User_id", "dbo.Users");
            DropForeignKey("dbo.Games", "User_id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "User_id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Game_id", "dbo.Games");
            DropForeignKey("dbo.Reviews", "Entity_id", "dbo.Entities");
            DropForeignKey("dbo.Games", "Entity_id", "dbo.Entities");
            DropForeignKey("dbo.Articles", "Game_id", "dbo.Games");
            DropForeignKey("dbo.Comments", "User_id", "dbo.Users");
            DropForeignKey("dbo.Articles", "User_id", "dbo.Users");
            DropIndex("dbo.Votes", new[] { "User_id" });
            DropIndex("dbo.Votes", new[] { "Entity_id" });
            DropIndex("dbo.Logs", new[] { "User_id" });
            DropIndex("dbo.Reviews", new[] { "Game_id" });
            DropIndex("dbo.Reviews", new[] { "User_id" });
            DropIndex("dbo.Reviews", new[] { "Entity_id" });
            DropIndex("dbo.Games", new[] { "User_id" });
            DropIndex("dbo.Games", new[] { "Entity_id" });
            DropIndex("dbo.Comments", new[] { "User_id" });
            DropIndex("dbo.Comments", new[] { "TargetEntity_Id" });
            DropIndex("dbo.Comments", new[] { "Entity_Id" });
            DropIndex("dbo.Entities", new[] { "Entity_Id" });
            DropIndex("dbo.Articles", new[] { "Game_id" });
            DropIndex("dbo.Articles", new[] { "User_id" });
            DropIndex("dbo.Articles", new[] { "Entity_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Logs");
            DropTable("dbo.Reviews");
            DropTable("dbo.Games");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Entities");
            DropTable("dbo.Articles");
        }
    }
}
