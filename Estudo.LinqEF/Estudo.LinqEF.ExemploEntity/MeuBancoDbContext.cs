using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntity
{
    public class MeuBancoDbContext : DbContext
    {
        public MeuBancoDbContext()
            : base(@"Data Source=(local);Initial Catalog=MeuBancoCodeFirst;User Id=sa;Password=saadmin;")
        {
            // Foi definido que o banco de dados sempre deve ser excluído quando uma entidade for alterada.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MeuBancoDbContext>());
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
    }
}
