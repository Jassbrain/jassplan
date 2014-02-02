namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxxx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "DoneDate", c => c.DateTime());
            AddColumn("dbo.JassActivityHistories", "DoneDate", c => c.DateTime());
            DropColumn("dbo.JassActivities", "Done");
            DropColumn("dbo.JassActivityHistories", "Done");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JassActivityHistories", "Done", c => c.DateTime());
            AddColumn("dbo.JassActivities", "Done", c => c.DateTime());
            DropColumn("dbo.JassActivityHistories", "DoneDate");
            DropColumn("dbo.JassActivities", "DoneDate");
        }
    }
}
