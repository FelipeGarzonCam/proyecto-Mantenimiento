namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncMantenimientos : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Mantenimiento", newName: "Mantenimientos");
            AddColumn("dbo.Mantenimientos", "Responsable", c => c.String(maxLength: 80));
            AddColumn("dbo.Mantenimientos", "Notas", c => c.String());
            AlterColumn("dbo.Mantenimientos", "Tipo", c => c.String(maxLength: 20));
            DropColumn("dbo.Mantenimientos", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mantenimientos", "Descripcion", c => c.String(maxLength: 250));
            AlterColumn("dbo.Mantenimientos", "Tipo", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Mantenimientos", "Notas");
            DropColumn("dbo.Mantenimientos", "Responsable");
            RenameTable(name: "dbo.Mantenimientos", newName: "Mantenimiento");
        }
    }
}
