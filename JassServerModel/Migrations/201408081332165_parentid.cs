namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "ParentID", c => c.Int());
            AddColumn("dbo.JassActivityHistories", "ParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "ParentID");
            DropColumn("dbo.JassActivities", "ParentID");
        }
    }
}
