namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "Status", c => c.String());
            AddColumn("dbo.JassActivityHistories", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "Status");
            DropColumn("dbo.JassActivities", "Status");
        }
    }
}
