using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Estudo.LinqEF.ExemploEntity
{
    public class BancoContext : DbContext
    {
        public DbSet<CategoriaProduto> Categorias { get; set; }

        public DbSet<SubCategoriaProduto> Subcategorias { get; set; }

        public BancoContext()
            : base(@"Data Source=(local);Initial Catalog=AdventureWorks2012;User Id=sa;Password=saadmin;")
        {
            Database.SetInitializer<BancoContext>(null);
        }
    }
}
