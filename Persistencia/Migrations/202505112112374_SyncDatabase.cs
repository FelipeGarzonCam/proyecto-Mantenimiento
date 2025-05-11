namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mantenimientos", "Tipo", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mantenimientos", "Tipo", c => c.String(maxLength: 20));
        }
    }
}
