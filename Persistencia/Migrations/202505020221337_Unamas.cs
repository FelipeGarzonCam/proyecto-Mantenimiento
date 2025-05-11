namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unamas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipo", "Cantidad", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equipo", "Cantidad", c => c.Int(nullable: false));
        }
    }
}
