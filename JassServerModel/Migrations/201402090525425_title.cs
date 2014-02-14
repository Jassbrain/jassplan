namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "title", c => c.String());
            AddColumn("dbo.JassActivities", "narrative", c => c.String());
            AddColumn("dbo.JassActivities", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.JassActivityHistories", "title", c => c.String());
            AddColumn("dbo.JassActivityHistories", "narrative", c => c.String());
            AddColumn("dbo.JassActivityHistories", "dateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "dateCreated");
            DropColumn("dbo.JassActivityHistories", "narrative");
            DropColumn("dbo.JassActivityHistories", "title");
            DropColumn("dbo.JassActivities", "dateCreated");
            DropColumn("dbo.JassActivities", "narrative");
            DropColumn("dbo.JassActivities", "title");
        }
    }
}
