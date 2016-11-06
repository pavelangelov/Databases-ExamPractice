namespace SocialNetwork.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friednships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstUserId = c.Int(nullable: false),
                        SecondUserId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApprovingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.FirstUserId)
                .ForeignKey("dbo.UserProfiles", t => t.SecondUserId)
                .Index(t => t.FirstUserId)
                .Index(t => t.SecondUserId)
                .Index(t => t.IsApproved);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        FileExtension = c.String(nullable: false, maxLength: 4),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendshipId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        SendingDate = c.DateTime(nullable: false),
                        SeeingDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.AuthorId)
                .ForeignKey("dbo.Friednships", t => t.FriendshipId)
                .Index(t => t.FriendshipId)
                .Index(t => t.AuthorId)
                .Index(t => t.SendingDate);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        PostingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostUserProfiles",
                c => new
                    {
                        Post_Id = c.Int(nullable: false),
                        UserProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_Id, t.UserProfile_Id })
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friednships", "SecondUserId", "dbo.UserProfiles");
            DropForeignKey("dbo.Friednships", "FirstUserId", "dbo.UserProfiles");
            DropForeignKey("dbo.PostUserProfiles", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.PostUserProfiles", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Messages", "FriendshipId", "dbo.Friednships");
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.UserProfiles");
            DropForeignKey("dbo.Images", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.PostUserProfiles", new[] { "UserProfile_Id" });
            DropIndex("dbo.PostUserProfiles", new[] { "Post_Id" });
            DropIndex("dbo.Messages", new[] { "SendingDate" });
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.Messages", new[] { "FriendshipId" });
            DropIndex("dbo.Images", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "UserName" });
            DropIndex("dbo.Friednships", new[] { "IsApproved" });
            DropIndex("dbo.Friednships", new[] { "SecondUserId" });
            DropIndex("dbo.Friednships", new[] { "FirstUserId" });
            DropTable("dbo.PostUserProfiles");
            DropTable("dbo.Posts");
            DropTable("dbo.Messages");
            DropTable("dbo.Images");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Friednships");
        }
    }
}
