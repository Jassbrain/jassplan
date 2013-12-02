namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activityhistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JassActivityHistories", "JassAreaID", "dbo.JassAreas");
            DropForeignKey("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID", "dbo.JassAreaHistories");
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaID" });
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaHistory_JassAreaHistoryID" });
            AddColumn("dbo.JassActivityHistories", "JassAreaHistoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.JassActivityHistories", "JassAreaHistoryID");
            AddForeignKey("dbo.JassActivityHistories", "JassAreaHistoryID", "dbo.JassAreaHistories", "JassAreaHistoryID", cascadeDelete: true);
            DropColumn("dbo.JassActivityHistories", "JassAreaID");
            DropColumn("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID", c => c.Int());
            AddColumn("dbo.JassActivityHistories", "JassAreaID", c => c.Int(nullable: false));
            DropForeignKey("dbo.JassActivityHistories", "JassAreaHistoryID", "dbo.JassAreaHistories");
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaHistoryID" });
            DropColumn("dbo.JassActivityHistories", "JassAreaHistoryID");
            CreateIndex("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID");
            CreateIndex("dbo.JassActivityHistories", "JassAreaID");
            AddForeignKey("dbo.JassActivityHistories", "JassAreaHistory_JassAreaHistoryID", "dbo.JassAreaHistories", "JassAreaHistoryID");
            AddForeignKey("dbo.JassActivityHistories", "JassAreaID", "dbo.JassAreas", "JassAreaID", cascadeDelete: true);
        }
    }
}
