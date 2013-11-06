namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AreaHistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassAreaHistories",
                c => new
                    {
                        JassAreaHistoryID = c.Int(nullable: false, identity: true),
                        JassAreaKey = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JassAreaHistoryID);
            
            CreateTable(
                "dbo.JassActivityHistories",
                c => new
                    {
                        JassActivityHistoryID = c.Int(nullable: false, identity: true),
                        JassActivityKey = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        JassAreaID = c.Int(nullable: false),
                        EstimatedDuration = c.Int(),
                        ActualDuration = c.Int(),
                        TodoToday = c.Boolean(nullable: false),
                        DoneToday = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Done = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        JassAreaHistory_JassAreaHistoryID = c.Int(),
                    })
                .PrimaryKey(t => t.JassActivityHistoryID)
                .ForeignKey("dbo.JassAreas", t => t.JassAreaID, cascadeDelete: true)
                .ForeignKey("dbo.JassAreaHistories", t => t.JassAreaHistory_JassAreaHistoryID)
                .Index(t => t.JassAreaID)
                .Index(t => t.JassAreaHistory_JassAreaHistoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID", "dbo.JassAreaHistories");
            DropForeignKey("dbo.JassActivityHistories", "JassAreaID", "dbo.JassAreas");
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaHistory_JassAreaHistoryID" });
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaID" });
            DropTable("dbo.JassActivityHistories");
            DropTable("dbo.JassAreaHistories");
        }
    }
}
