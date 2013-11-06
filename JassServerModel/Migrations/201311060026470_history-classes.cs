namespace JassServerModel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class historyclasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JassAreas", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JassAreas", "Description");
        }
    }
}
