namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reivew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassActivityReviews",
                c => new
                    {
                        JassActivityReviewID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.JassActivityReviewID);
            
            AddColumn("dbo.JassActivityHistories", "JassActivityReviewID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassActivityHistories", "JassActivityReviewID");
            DropTable("dbo.JassActivityReviews");
        }
    }
}
