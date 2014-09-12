namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estimatedStartHour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "EstimatedStartHour", c => c.Int());
            AddColumn("dbo.JassActivityHistories", "EstimatedStartHour", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "EstimatedStartHour");
            DropColumn("dbo.JassActivities", "EstimatedStartHour");
        }
    }
}
