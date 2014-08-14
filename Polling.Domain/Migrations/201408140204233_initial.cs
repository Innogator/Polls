namespace Polling.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UrlSlug = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Poll",
                c => new
                    {
                        PollID = c.Int(nullable: false, identity: true),
                        PollQuestion = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 500),
                        PubDate = c.DateTime(nullable: false),
                        UrlSlug = c.String(maxLength: 200),
                        Meta = c.String(maxLength: 50),
                        CategoryID = c.Int(),
                        Author_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PollID)
                .ForeignKey("dbo.User", t => t.Author_UserID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .Index(t => t.CategoryID)
                .Index(t => t.Author_UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Reputation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PollID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CommentText = c.String(),
                        DatePub = c.DateTime(nullable: false),
                        ParentCommentID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Poll", t => t.PollID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.PollID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        VoteID = c.Int(nullable: false, identity: true),
                        PollID = c.Int(nullable: false),
                        ChoiceID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        DateVoted = c.DateTime(nullable: false),
                        Option_OptionID = c.Int(),
                    })
                .PrimaryKey(t => t.VoteID)
                .ForeignKey("dbo.Option", t => t.Option_OptionID)
                .ForeignKey("dbo.Poll", t => t.PollID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.PollID)
                .Index(t => t.UserID)
                .Index(t => t.Option_OptionID);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        OptionID = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 50),
                        PollID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionID)
                .ForeignKey("dbo.Poll", t => t.PollID, cascadeDelete: true)
                .Index(t => t.PollID);
            
            CreateTable(
                "dbo.PollTag",
                c => new
                    {
                        PollID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PollID, t.TagID })
                .ForeignKey("dbo.Poll", t => t.PollID, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagID, cascadeDelete: true)
                .Index(t => t.PollID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UrlSlug = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PollTag", "TagID", "dbo.Tag");
            DropForeignKey("dbo.PollTag", "PollID", "dbo.Poll");
            DropForeignKey("dbo.Poll", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Vote", "UserID", "dbo.User");
            DropForeignKey("dbo.Vote", "PollID", "dbo.Poll");
            DropForeignKey("dbo.Vote", "Option_OptionID", "dbo.Option");
            DropForeignKey("dbo.Option", "PollID", "dbo.Poll");
            DropForeignKey("dbo.Poll", "Author_UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "PollID", "dbo.Poll");
            DropIndex("dbo.PollTag", new[] { "TagID" });
            DropIndex("dbo.PollTag", new[] { "PollID" });
            DropIndex("dbo.Option", new[] { "PollID" });
            DropIndex("dbo.Vote", new[] { "Option_OptionID" });
            DropIndex("dbo.Vote", new[] { "UserID" });
            DropIndex("dbo.Vote", new[] { "PollID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropIndex("dbo.Comment", new[] { "PollID" });
            DropIndex("dbo.Poll", new[] { "Author_UserID" });
            DropIndex("dbo.Poll", new[] { "CategoryID" });
            DropTable("dbo.Tag");
            DropTable("dbo.PollTag");
            DropTable("dbo.Option");
            DropTable("dbo.Vote");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Poll");
            DropTable("dbo.Category");
        }
    }
}
