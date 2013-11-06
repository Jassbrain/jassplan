namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassActivities",
                c => new
                    {
                        JassActivityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        JassAreaID = c.Int(nullable: false),
                        EstimatedDuration = c.Int(),
                        ActualDuration = c.Int(),
                        TodoToday = c.Boolean(nullable: false),
                        DoneToday = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        DoneDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.JassActivityID)
                .ForeignKey("dbo.JassAreas", t => t.JassAreaID, cascadeDelete: true)
                .Index(t => t.JassAreaID);
            
            CreateTable(
                "dbo.JassAreas",
                c => new
                    {
                        JassAreaID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.JassAreaID);
            
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
                .PrimaryKey(t => t.JassActivityLogID)
                .ForeignKey("dbo.JassAreas", t => t.JassAreaID, cascadeDelete: true)
                .Index(t => t.JassAreaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JassActivityLogs", "JassAreaID", "dbo.JassAreas");
            DropForeignKey("dbo.JassActivities", "JassAreaID", "dbo.JassAreas");
            DropIndex("dbo.JassActivityLogs", new[] { "JassAreaID" });
            DropIndex("dbo.JassActivities", new[] { "JassAreaID" });
            DropTable("dbo.JassActivityLogs");
            DropTable("dbo.JassAreas");
            DropTable("dbo.JassActivities");
        }
    }
}
