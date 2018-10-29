using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Consulta simples - por ferramenta
            Console.WriteLine("Categorias por ferramenta!");
            using (AdventureWorks2012Entities dc = new AdventureWorks2012Entities())
            {
                foreach (var item in dc.ProductSubcategory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            #endregion

            #region Consulta simples - por código
            Console.WriteLine("\n\nCategorias por código!");
            using (BancoContext dc = new BancoContext())
            {
                foreach (var item in dc.Categorias)
                {
                    Console.WriteLine(item.Name);
                }
            }
            #endregion

            #region Code First - por código
            Console.WriteLine("\n\nFuncionarios:");
            using (MeuBancoDbContext meuBancodc = new MeuBancoDbContext())
            {
                foreach (var item in meuBancodc.Funcionarios.Include("Departamento"))
                {
                    Console.WriteLine("Funcionário: {0}, Departamento: {1}", item.Nome, item.Departamento.Nome);
                }
            }
            #endregion

            // Foi adicionando uma classe cópia da classe Funcionario, para exemplificar o uso de migrations.
            // Como as classes são diferentes, não houve substituição de uma classe pela outra pelas migrations.

            Console.ReadKey();
        }
    }
}
