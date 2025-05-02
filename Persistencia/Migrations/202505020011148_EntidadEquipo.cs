namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntidadEquipo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        EquipoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId);
            
            CreateTable(
                "dbo.Mantenimientoes",
                c => new
                    {
                        MantenimientoId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(maxLength: 250),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MantenimientoId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mantenimientoes", "EquipoId", "dbo.Equipoes");
            DropIndex("dbo.Mantenimientoes", new[] { "EquipoId" });
            DropTable("dbo.Mantenimientoes");
            DropTable("dbo.Equipoes");
        }
    }
}
