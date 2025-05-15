namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCamposMantenimiento : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Mantenimientos", newName: "Mantenimiento");
            AddColumn("dbo.Mantenimiento", "OtNumero", c => c.String(maxLength: 25));
            AddColumn("dbo.Mantenimiento", "TrabajoRealizado", c => c.String(maxLength: 500));
            AddColumn("dbo.Mantenimiento", "CostoTotal", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Mantenimiento", "Descripcion", c => c.String(maxLength: 500));
            DropColumn("dbo.Mantenimiento", "Responsable");
            DropColumn("dbo.Mantenimiento", "Notas");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mantenimiento", "Notas", c => c.String());
            AddColumn("dbo.Mantenimiento", "Responsable", c => c.String(maxLength: 80));
            DropColumn("dbo.Mantenimiento", "Descripcion");
            DropColumn("dbo.Mantenimiento", "CostoTotal");
            DropColumn("dbo.Mantenimiento", "TrabajoRealizado");
            DropColumn("dbo.Mantenimiento", "OtNumero");
            RenameTable(name: "dbo.Mantenimiento", newName: "Mantenimientos");
        }
    }
}
