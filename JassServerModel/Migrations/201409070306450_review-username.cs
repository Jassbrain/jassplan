namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivityReviews", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityReviews", "UserName");
        }
    }
}
