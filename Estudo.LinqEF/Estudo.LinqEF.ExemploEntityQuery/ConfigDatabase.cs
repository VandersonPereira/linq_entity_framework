using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntityQuery
{
    class ConfigDatabase : DropCreateDatabaseIfModelChanges<BancoCodeFirst>
    {
        protected override void Seed(BancoCodeFirst context)
        {
            var departamentos = new List<Departamentos>
        {
            new Departamentos { Nome = "RH" },
            new Departamentos { Nome = "Desenvolvimento" },
            new Departamentos { Nome = "Marketing"},
            new Departamentos { Nome = "Gerência"}
        };

            departamentos.ForEach(c => context.Departamentos.Add(c));
            context.SaveChanges();

            var funcionarios = new List<Funcionario>
        {
            new Funcionario {
                Nome = "Carlos Silva",
                Cargo = "Analista",
                Salario = 4500,
                Departamento = departamentos.Single(d => d.Nome.Contains("Desenvolvimento"))
            },
            new Funcionario {
                Nome = "Maria Santos",
                Cargo = "Psicologa",
                Salario = 2400,
                Departamento = departamentos.Single(d => d.Nome.Contains("RH"))
            },
            new Funcionario {
                Nome = "José Sousa",
                Cargo = "CEO",
                Salario = 12000,
                Departamento = departamentos.Single(d => d.Nome.Contains("Gerência"))
            },
            new Funcionario {
                Nome = "Ana Monteiro",
                Cargo = "Redatora",
                Salario = 2000,
                Departamento = departamentos.Single(d => d.Nome.Contains("Marketing"))
            },
            new Funcionario {
                Nome = "Elis Regina",
                Cargo = "Estagiária",
                Salario = 1050.1M,
                Departamento = departamentos.Single(d => d.Nome.Contains("Desenvolvimento"))
            },
        };

            funcionarios.ForEach(s => context.Funcionarios.Add(s));
            context.SaveChanges();
        }
    }
}
