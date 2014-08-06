namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "Flag", c => c.String());
            AddColumn("dbo.JassActivityHistories", "Flag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "Flag");
            DropColumn("dbo.JassActivities", "Flag");
        }
    }
}
