using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToSQL.Transaction
{
    class Program
    {
        private static AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
        static void Main(string[] args)
        {
            dc.DeferredLoadingEnabled = false;
            int op = 1;
            while (op != 0)
            {
                Console.Clear();
                Console.WriteLine("Dentre as alternativas abaixo:");
                Console.WriteLine("1 - Consultar");
                Console.WriteLine("2 - Incluir");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("5 - Incluir Valor nulo");
                Console.WriteLine("6 - Efetivar alterações no banco");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("Selecione uma opcao:");
                if (!Int32.TryParse(Console.ReadLine().ToString(), out op))
                    op = -1;

                switch (op)
                {
                    case 1:
                        RealizarConsulta();
                        break;
                    case 2:
                        RealizarInclusao();
                        break;
                    case 3:
                        RealizarAlteracao();
                        break;
                    case 4:
                        RealizarExclusao();
                        break;
                    case 5:
                        RealizarInclusaoNula();
                        break;
                    case 6:
                        EfetivarAlteracoes();
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!\n\n");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RealizarInclusao()
        {
            string categoria;

            Console.Clear();
            Console.WriteLine("Informe uma categoria:");
            categoria = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(categoria))
            {
                ProductCategory pc = new ProductCategory();
                pc.Name = categoria;
                pc.rowguid = Guid.NewGuid();
                pc.ModifiedDate = DateTime.Now;

                dc.ProductCategories.InsertOnSubmit(pc);

                Console.WriteLine("O registro {0}, incluido com sucesso da memória\n", pc.Name);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void RealizarConsulta()
        {
            var result = from pc in dc.ProductCategories
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            Console.Clear();
            Console.WriteLine("Categorias:");
            foreach (var item in result)
            {
                Console.WriteLine("{0} - {1}", item.ProductCategoryID, item.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void RealizarAlteracao()
        {
            string categoria;
            int id;

            Console.Clear();
            Console.Write("Informe o id categoria que será alterada:");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                ProductCategory pc = dc.ProductCategories.Single(p => p.ProductCategoryID == id);

                //Em sintaxe de pesquisa ficaria da seguinte forma:
                //
                //ProductCategory pc2 = (from p in dc.ProductCategories
                //                       where p.ProductCategoryID == id
                //                       select p).Single();

                Console.WriteLine("\n\nInforme um novo nome para a categoria {0}:", pc.Name);
                categoria = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(categoria))
                {
                    pc.Name = categoria;
                    pc.ModifiedDate = DateTime.Now;

                    Console.WriteLine("O registro {0}, alterado com sucesso na memória\n", pc.ProductCategoryID);
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void RealizarExclusao()
        {
            int id;

            Console.Clear();
            Console.Write("Informe o id categoria que será excluida:");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                ProductCategory pc = dc.ProductCategories.Single(p => p.ProductCategoryID == id);

                //Em sintaxe de pesquisa ficaria da seguinte forma:
                //
                //ProductCategory pc2 = (from p in dc.ProductCategories
                //                       where p.ProductCategoryID == id
                //                       select p).Single();

                dc.ProductCategories.DeleteOnSubmit(pc);
                Console.WriteLine("Registro excluido com sucesso da memória\n");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void EfetivarAlteracoes()
        {

            if (dc.Connection.State != System.Data.ConnectionState.Open)
                dc.Connection.Open();
            using (dc.Transaction = dc.Connection.BeginTransaction())
            {
                try
                {
                    dc.SubmitChanges();
                    dc.Transaction.Commit();
                    Console.WriteLine("Dados salvos no banco!");
                }
                catch (Exception ex)
                {
                    dc.Transaction.Rollback();
                    Console.WriteLine("Ocorreu o seguinte erro:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        static void RealizarInclusaoNula()
        {
            Console.Clear();
            ProductCategory pc = new ProductCategory();
            //pc.Nome não é informado
            pc.rowguid = Guid.NewGuid();
            pc.ModifiedDate = DateTime.Now;

            dc.ProductCategories.InsertOnSubmit(pc);

            Console.WriteLine("O registro nulo, incluido com sucesso da memória\n");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
