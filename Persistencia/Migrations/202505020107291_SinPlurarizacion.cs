namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SinPlurarizacion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Equipoes", newName: "Equipo");
            RenameTable(name: "dbo.Mantenimientoes", newName: "Mantenimiento");
            RenameTable(name: "dbo.Usuarios", newName: "Usuario");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Usuario", newName: "Usuarios");
            RenameTable(name: "dbo.Mantenimiento", newName: "Mantenimientoes");
            RenameTable(name: "dbo.Equipo", newName: "Equipoes");
        }
    }
}
