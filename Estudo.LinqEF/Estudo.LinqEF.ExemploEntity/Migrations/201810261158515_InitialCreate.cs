namespace Estudo.LinqEF.ExemploEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        FuncionarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cargo = c.String(),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Departamento_DepartamentosId = c.Int(),
                    })
                .PrimaryKey(t => t.FuncionarioId)
                .ForeignKey("dbo.Departamentos", t => t.Departamento_DepartamentosId)
                .Index(t => t.Departamento_DepartamentosId);
            
            CreateTable(
                "dbo.Departamentos",
                c => new
                    {
                        DepartamentosId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.DepartamentosId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Funcionarios", new[] { "Departamento_DepartamentosId" });
            DropForeignKey("dbo.Funcionarios", "Departamento_DepartamentosId", "dbo.Departamentos");
            DropTable("dbo.Departamentos");
            DropTable("dbo.Funcionarios");
        }
    }
}
