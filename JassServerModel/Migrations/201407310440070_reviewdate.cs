namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivityReviews", "ReviewYear", c => c.Int(nullable: false));
            AddColumn("dbo.JassActivityReviews", "ReviewMonth", c => c.Int(nullable: false));
            AddColumn("dbo.JassActivityReviews", "ReviewDay", c => c.Int(nullable: false));
            AddColumn("dbo.JassActivityReviews", "ReviewDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityReviews", "ReviewDate");
            DropColumn("dbo.JassActivityReviews", "ReviewDay");
            DropColumn("dbo.JassActivityReviews", "ReviewMonth");
            DropColumn("dbo.JassActivityReviews", "ReviewYear");
        }
    }
}
