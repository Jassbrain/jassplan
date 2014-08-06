namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wentback : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JassActivityHistories", "JassActivityReviewID", "dbo.JassActivityReviews");
            DropIndex("dbo.JassActivityHistories", new[] { "JassActivityReviewID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.JassActivityHistories", "JassActivityReviewID");
            AddForeignKey("dbo.JassActivityHistories", "JassActivityReviewID", "dbo.JassActivityReviews", "JassActivityReviewID");
        }
    }
}
