using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntityQuery
{
    public class BancoCodeFirst : DbContext
    {
        public BancoCodeFirst() : base("Data Source=(local);Initial Catalog=BancoCodeFirst;User Id=sa;Password=saadmin")
        {
            Database.SetInitializer<BancoCodeFirst>(new ConfigDatabase());
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
    }
}
