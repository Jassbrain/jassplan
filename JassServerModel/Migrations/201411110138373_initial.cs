namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassActivities",
                c => new
                    {
                        JassActivityID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        UserName = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        title = c.String(),
                        narrative = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                        EstimatedDuration = c.Int(),
                        EstimatedStartHour = c.Int(),
                        ActualDuration = c.Int(),
                        TodoToday = c.Boolean(nullable: false),
                        DoneToday = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        DoneDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.JassActivityID);
            
            CreateTable(
                "dbo.JassActivityHistories",
                c => new
                    {
                        JassActivityHistoryID = c.Int(nullable: false, identity: true),
                        JassActivityID = c.Int(nullable: false),
                        JassActivityReviewID = c.Int(),
                        TimeStamp = c.DateTime(nullable: false),
                        ParentID = c.Int(),
                        UserName = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        title = c.String(),
                        narrative = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                        EstimatedDuration = c.Int(),
                        EstimatedStartHour = c.Int(),
                        ActualDuration = c.Int(),
                        TodoToday = c.Boolean(nullable: false),
                        DoneToday = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        DoneDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.JassActivityHistoryID);
            
            CreateTable(
                "dbo.JassActivityReviews",
                c => new
                    {
                        JassActivityReviewID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ReviewYear = c.Int(nullable: false),
                        ReviewMonth = c.Int(nullable: false),
                        ReviewDay = c.Int(nullable: false),
                        ReviewDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JassActivityReviewID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JassActivityReviews");
            DropTable("dbo.JassActivityHistories");
            DropTable("dbo.JassActivities");
        }
    }
}
