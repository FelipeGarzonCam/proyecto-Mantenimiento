namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTarjetaMaestraFieldsToEquipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipo", "CodigoInterno", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Equipo", "Marca", c => c.String(maxLength: 60));
            AddColumn("dbo.Equipo", "Modelo", c => c.String(maxLength: 60));
            AddColumn("dbo.Equipo", "NumeroSerie", c => c.String(maxLength: 60));
            AddColumn("dbo.Equipo", "FechaAdquisicion", c => c.DateTime());
            AddColumn("dbo.Equipo", "Ubicacion", c => c.String(maxLength: 80));
            AddColumn("dbo.Equipo", "PotenciaHP", c => c.Double());
            AddColumn("dbo.Equipo", "Capacidad", c => c.Double());
            AddColumn("dbo.Equipo", "PesoKg", c => c.Double());
            AddColumn("dbo.Equipo", "Criticidad", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipo", "Criticidad");
            DropColumn("dbo.Equipo", "PesoKg");
            DropColumn("dbo.Equipo", "Capacidad");
            DropColumn("dbo.Equipo", "PotenciaHP");
            DropColumn("dbo.Equipo", "Ubicacion");
            DropColumn("dbo.Equipo", "FechaAdquisicion");
            DropColumn("dbo.Equipo", "NumeroSerie");
            DropColumn("dbo.Equipo", "Modelo");
            DropColumn("dbo.Equipo", "Marca");
            DropColumn("dbo.Equipo", "CodigoInterno");
        }
    }
}
