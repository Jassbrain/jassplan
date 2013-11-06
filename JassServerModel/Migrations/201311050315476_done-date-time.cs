namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donedatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassActivities", "Done", c => c.DateTime());
            DropColumn("dbo.JassActivities", "DoneDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JassActivities", "DoneDateTime", c => c.DateTime());
            DropColumn("dbo.JassActivities", "Done");
        }
    }
}
