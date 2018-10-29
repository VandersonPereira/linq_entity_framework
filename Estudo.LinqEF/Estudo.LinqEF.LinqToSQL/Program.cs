using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            AdventureWorks dc = new AdventureWorks(@"Data Source=(local);Initial Catalog=AdventureWorks2012;User Id=sa;Password=saadmin;");

            #region Consulta com uma tabela (Consulta manual - via código)
            var result = from pc in dc.ProductCategories
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Consulta com relacionamento chave primária/chave estrangeira (Consulta manual - via código)
            var result2 = from pc in dc.ProductCategories
                         orderby pc.Name
                         select new { Nome = pc.Name, SubCategorias = pc.ProductSubcategories };

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name).Select(pc => new {Nome = pc.Name, SubCategorias = pc.ProductSubcategories });

            foreach (var item in result2)
            {
                Console.WriteLine("******************************");
                Console.WriteLine("Subcategorias de {0}:", item.Nome);
                foreach (var sub in item.SubCategorias)
                {
                    Console.WriteLine("{0}", sub.Name);
                }
                Console.WriteLine("******************************");
                Console.WriteLine("");
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Consulta (via ferramenta)
            AdventureWorksByToolDataContext dc2 = new AdventureWorksByToolDataContext();

            // Retorna o comando SQL
            dc2.Log = Console.Out;

            var result3 = from pc in dc2.ProductCategories
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            foreach (var item in result3)
            {
                Console.WriteLine(item.Name);
            }
            #endregion

            #region Inclusão, alteração, deleção e consulta
            int op = 1;
            while (op != 0)
            {
                Console.Clear();
                Console.WriteLine("Dentre as alternativas abaixo:");
                Console.WriteLine("1 - Consultar");
                Console.WriteLine("2 - Incluir");
                Console.WriteLine("3 - Alterar");
                Console.WriteLine("4 - Excluir");
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
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!\n\n");
                        Console.ReadKey();
                        break;
                }
            }
            #endregion

            #region Lidando com procedures (via código)
            IMultipleResults resultProcedure = dc.GetCategory(1);
            Console.WriteLine("Categorias:");
            foreach (var item in resultProcedure.GetResult<CodeFirst.ProductCategory>())
            {
                Console.WriteLine("{0} - {1}", item.ProductCategoryID, item.Name);
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Lidando com procedures (via ferramenta)
            AdventureWorksByToolDataContext dcFerramenta = new AdventureWorksByToolDataContext();
            var resultProcedureFerramenta = from pc in dcFerramenta.usp_GetCategory(1)
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.usp_GetCategory(1).OrderBy(pc => pc.Name);

            Console.WriteLine("Categorias:");
            foreach (var item in result)
            {
                Console.WriteLine("{0} - {1}", item.ProductCategoryID, item.Name);
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Lidando com functions (via código)
            var resultFunction = from p in dc.FindSubcategories("bike")
                         orderby p.Name descending
                         select new { p.Name, p.ProductSubcategoryID, MaxPrice = dc.MaxPriceBySubcategory(p.ProductSubcategoryID) };

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.FindSubcategories("bike").OrderByDescending(pc => pc.Name).Select(p => new { p.Name, p.ProductSubcategoryID, MaxPrice = dc.MaxPriceBySubcategory(p.ProductSubcategoryID) });

            Console.WriteLine("Subcategorias com bike no nome:");
            foreach (var item in resultFunction)
            {
                Console.WriteLine("{0} - {1} - {2}", item.ProductSubcategoryID, item.Name, item.MaxPrice.GetValueOrDefault(0).ToString("C"));
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Lidando com functions (via ferramenta)
            AdventureWorksByToolDataContext dcFunction = new AdventureWorksByToolDataContext();
            var resultFunction2 = from pc in dcFunction.FindProductSubcategoriesByName("Shorts")
                         orderby pc.Name
                         select new { pc.Name, pc.ProductSubcategoryID, MaxPrice = dcFunction.GetMaxPriceBySubcategory(pc.ProductSubcategoryID) };

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.FindProductSubcategoriesByName("Shorts").OrderByDescending(pc => pc.Name).Select(p => new { p.Name, p.ProductSubcategoryID, MaxPrice = dc.GetMaxPriceBySubcategory(p.ProductSubcategoryID) });

            Console.WriteLine("Subcategorias com shorts no nome:");
            foreach (var item in resultFunction2)
            {
                Console.WriteLine("{0} - {1} - {2}", item.ProductSubcategoryID, item.Name, item.MaxPrice.GetValueOrDefault(0).ToString("C"));
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Consultas compiladas
            AdventureWorksByToolDataContext dcComplida = new AdventureWorksByToolDataContext();
            Table<ProductSubcategory> ProductSubcategories = dcComplida.GetTable<ProductSubcategory>();

            var query = CompiledQuery.Compile(
                                (DataContext context, string filterName) =>
                                from p in ProductSubcategories
                                where p.Name.Contains(filterName)
                                select new { p.Name, p.ProductSubcategoryID });

            Console.WriteLine("Subcategorias com Bike no nome:");
            foreach (var item in query(dc, "Bike"))
            {
                Console.WriteLine("{0} - {1}", item.ProductSubcategoryID, item.Name);
            }

            Console.WriteLine("\n\nSubcategorias com Shorts no nome:");
            foreach (var item in query(dc, "Shorts"))
            {
                Console.WriteLine("{0} - {1}", item.ProductSubcategoryID, item.Name);
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            #region Executando consultas diretas
            AdventureWorksByToolDataContext dcDiretas = new AdventureWorksByToolDataContext();

            var resultDiretas = dcDiretas.ExecuteQuery<ProductSubcategory>(@"SELECT TOP(50)PERCENT ProductSubcategoryID, Name, rowguid, ModifiedDate FROM Production.ProductSubcategory");

            Console.WriteLine("50% das subcategorias cadastradas:");
            foreach (var item in resultDiretas)
            {
                Console.WriteLine("{0} - {1}", item.ProductSubcategoryID, item.Name);
            }
            #endregion

            Console.WriteLine("-------------------------------------------------");

            Console.ReadKey();
        }

        static void RealizarInclusao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
            string categoria;

            Console.WriteLine("Informe uma categoria:");
            categoria = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(categoria))
            {
                ProductCategory pc = new ProductCategory();
                pc.Name = categoria;
                pc.rowguid = Guid.NewGuid();
                pc.ModifiedDate = DateTime.Now;

                dc.ProductCategories.InsertOnSubmit(pc);
                dc.SubmitChanges();

                Console.WriteLine("O registro {0}, incluído com sucesso\n", pc.ProductCategoryID);
            }
        }

        static void RealizarConsulta()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();

            var result = from pc in dc.ProductCategories
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            Console.WriteLine("Categorias:");
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void RealizarAlteracao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
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

                    dc.SubmitChanges();

                    Console.WriteLine("O registro {0}, alterado com sucesso\n", pc.ProductCategoryID);
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void RealizarExclusao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
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
                dc.SubmitChanges();

                Console.WriteLine("Registro excluio com sucesso\n");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
