namespace Pedidos.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crearbasededatos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LineaPedidoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Producto_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.Producto_Id)
                .Index(t => t.Producto_Id);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        PrecioCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioAlPorMenor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NombreCliente = c.String(nullable: false, maxLength: 200),
                        Dirección = c.String(nullable: false, maxLength: 2000),
                        Estado = c.String(nullable: false, maxLength: 50),
                        CodigoPostal = c.String(nullable: false),
                        Enviado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineaPedidoes", "Producto_Id", "dbo.Productoes");
            DropIndex("dbo.LineaPedidoes", new[] { "Producto_Id" });
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Productoes");
            DropTable("dbo.LineaPedidoes");
        }
    }
}
