namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_area : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JassActivities", "JassAreaID", "dbo.JassAreas");
            DropForeignKey("dbo.JassActivityHistories", "JassAreaHistoryID", "dbo.JassAreaHistories");
            DropIndex("dbo.JassActivities", new[] { "JassAreaID" });
            DropIndex("dbo.JassActivityHistories", new[] { "JassAreaHistoryID" });
            AddColumn("dbo.JassActivityHistories", "JassActivityID", c => c.Int(nullable: false));
            DropColumn("dbo.JassActivities", "JassAreaID");
            DropColumn("dbo.JassActivityHistories", "JassActivityKey");
            DropColumn("dbo.JassActivityHistories", "JassAreaHistoryID");
            DropTable("dbo.JassAreas");
            DropTable("dbo.JassAreaHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JassAreaHistories",
                c => new
                    {
                        JassAreaHistoryID = c.Int(nullable: false, identity: true),
                        JassAreaKey = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.JassAreaHistoryID);
            
            CreateTable(
                "dbo.JassAreas",
                c => new
                    {
                        JassAreaID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.JassAreaID);
            
            AddColumn("dbo.JassActivityHistories", "JassAreaHistoryID", c => c.Int(nullable: false));
            AddColumn("dbo.JassActivityHistories", "JassActivityKey", c => c.Int(nullable: false));
            AddColumn("dbo.JassActivities", "JassAreaID", c => c.Int(nullable: false));
            DropColumn("dbo.JassActivityHistories", "JassActivityID");
            CreateIndex("dbo.JassActivityHistories", "JassAreaHistoryID");
            CreateIndex("dbo.JassActivities", "JassAreaID");
            AddForeignKey("dbo.JassActivityHistories", "JassAreaHistoryID", "dbo.JassAreaHistories", "JassAreaHistoryID", cascadeDelete: true);
            AddForeignKey("dbo.JassActivities", "JassAreaID", "dbo.JassAreas", "JassAreaID", cascadeDelete: true);
        }
    }
}
