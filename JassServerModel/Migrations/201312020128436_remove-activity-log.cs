namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeactivitylog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JassActivityLogs", "JassAreaID", "dbo.JassAreas");
            DropIndex("dbo.JassActivityLogs", new[] { "JassAreaID" });
            DropTable("dbo.JassActivityLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JassActivityLogs",
                c => new
                    {
                        JassActivityLogID = c.Int(nullable: false, identity: true),
                        Logged = c.DateTime(nullable: false),
                        JassActivityID = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.JassActivityLogID);
            
            CreateIndex("dbo.JassActivityLogs", "JassAreaID");
            AddForeignKey("dbo.JassActivityLogs", "JassAreaID", "dbo.JassAreas", "JassAreaID", cascadeDelete: true);
        }
    }
}
