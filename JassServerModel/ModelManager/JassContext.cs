using System.Data.Entity;
using Jassplan.Model;

namespace Jassplan.ModelManager
{
    public class JassContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Jass.Models.JassContext>());

        public JassContext() : base("name=JassContext")
        {
        }

        public DbSet<JassArea> JassAreas { get; set; }

        public DbSet<JassAreaHistory> JassAreaHistories { get; set; }

        public DbSet<JassActivity> JassActivities { get; set; }

        public DbSet<JassActivityHistory> JassActivityHistories { get; set; }

    }
}
