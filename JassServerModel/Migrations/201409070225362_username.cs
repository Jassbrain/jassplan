namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "UserName", c => c.String());
            AddColumn("dbo.JassActivityHistories", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "UserName");
            DropColumn("dbo.JassActivities", "UserName");
        }
    }
}
