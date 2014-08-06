namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newnew : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.JassActivityHistories", "JassActivityReviewID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JassActivityHistories", "JassActivityReviewID", "dbo.JassActivityReviews");
        }
    }
}
