using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Estudo.LinqEF.LinqToDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DataSets não tipados
            DataSet dsAdventure = PopularDataSet();
            DataTable dtProduto = dsAdventure.Tables["Product"];

            IEnumerable<DataRow> produtos =
                from p in dtProduto.AsEnumerable()
                where p.Field<string>("Color") == "Black"
                orderby p.Field<string>("Name") descending
                select p;

            //Em sintaxe de método ficaria da seguinte forma:
            //IEnumerable<DataRow> produtos = dtProduto.AsEnumerable().Where(p => p.Field<string>("Color") == "Black").OrderByDescending(p => p.Field<string>("Name"));

            foreach (DataRow dr in produtos)
            {
                Console.WriteLine("{0} \t- {1}", dr["ProductNumber"], dr["Name"]);
            }
            #endregion

            #region DataSets tipados
            dsAdventure dsAdventureTipado = PopularDataSetTipado();
            dsAdventure.ProductDataTable dtProdutoTipado = dsAdventureTipado.Product;

            IEnumerable<dsAdventure.ProductRow> produtosTipado =
                from p in dtProdutoTipado.AsEnumerable()
                where !p.IsColorNull() && p.Color == "Black"
                orderby p.Name descending
                select p;

            //Em sintaxe de método ficaria da seguinte forma:
            //IEnumerable<DataRow> produtos = dtProduto.AsEnumerable().Where(p => !p.IsColorNull() && p.Color == "Black").OrderByDescending(p => p.Name));

            foreach (dsAdventure.ProductRow dr in produtosTipado)
            {
                Console.WriteLine("{0} \t- {1}", dr.ProductNumber, dr.Name);
            }

            #endregion

            #region Populando objetos com Linq to DataSet
            List<Produto> produtosToObject = (
                from p in dtProdutoTipado.AsEnumerable()
                where !p.IsColorNull() && p.Color == "Black"
                orderby p.Name descending
                select new Produto
                {
                    Id = p.ProductID,
                    Nome = p.Name,
                    Numero = p.ProductNumber
                }).ToList();

            //Em sintaxe de método ficaria da seguinte forma:
            //List<Produto> produtos = dtProduto.AsEnumerable()
            //                                .Where(p => !p.IsColorNull() && p.Color == "Black")
            //                                .OrderByDescending(p => p.Field<string>("Name"))
            //                                .Select(p => new Produto
            //                                {
            //                                    Id = p.ProductID,
            //                                    Nome = p.Name,
            //                                    Numero = p.ProductNumber
            //                                }).ToList();

            foreach (Produto p in produtosToObject)
            {
                Console.WriteLine("{0} \t- {1}", p.Numero, p.Nome);
            }
            
            #endregion

            Console.ReadKey();
        }

        static DataSet PopularDataSet()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=SSPI";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "SELECT * FROM Production.Product";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds, "Product");

            return ds;
        }

        static dsAdventure PopularDataSetTipado()
        {
            dsAdventureTableAdapters.ProductTableAdapter da = new dsAdventureTableAdapters.ProductTableAdapter();

            dsAdventure ds = new dsAdventure();

            da.Fill(ds.Product);
            return ds;

            //Quando o DataTable é configurado na mão no DataSet é necessário executar o código abaixo para carregá-lo:
            /*
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=SSPI";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM Production.Product";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dsAdventure ds = new dsAdventure();
                da.Fill(ds, ds.Product.TableName);
                return ds;
                */
        }
    }
}
