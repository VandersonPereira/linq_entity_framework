using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoLinq._01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CONSULTA SIMPLES, SEM WHERE
            ///* ********* CONSULTA SIMPLES, SEM WHERE ******** */
            //Console.WriteLine("********* 1. CONSULTA SIMPLES, SEM WHERE ********");

            //// 1. Obtém a fonte de dados
            //int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //// 2. Cria a consulta
            //var querySemWhere = from num in numeros
            //                    select num;

            //// 3. Recupera o resultado da consulta
            //foreach (var numero in querySemWhere)
            //    Console.WriteLine(numero);

            //Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE
            ///* ********* CONSULTA SIMPLES, COM WHERE ******** */
            //Console.WriteLine("********* 2. CONSULTA SIMPLES, COM WHERE ********");

            //// 2. Cria a consulta
            //var queryComWhere = from num in numeros
            //                    where (num % 2) == 0 // Pode-se usar operadores && e ||
            //                    select num;

            //// 3. Recupera o resultado da consulta
            //foreach (var numero in queryComWhere)
            //    Console.WriteLine(numero);

            //Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE E ORDER BY 
            ///* ********* CONSULTA SIMPLES, COM WHERE E ORDER BY ******** */
            //Console.WriteLine("********* 3. CONSULTA SIMPLES, COM WHERE E ORDER BY ********");

            //// 2. Cria a consulta
            //var queryComWhereComOrderBy = from num in numeros
            //                    where (num % 2) == 0 // Pode-se usar operadores && e ||
            //                    orderby num descending
            //                    select num;

            //// 3. Recupera o resultado da consulta
            //foreach (var numero in queryComWhereComOrderBy)
            //    Console.WriteLine(numero);

            //Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE E GROUP BY
            ///* ********* CONSULTA SIMPLES, COM WHERE E GROUP BY ******** */
            //Console.WriteLine("********* 4. CONSULTA SIMPLES, COM WHERE E GROUP BY ********");

            //// 1. Obtém a fonte de dados
            //Dictionary<string, int> pessoas = new Dictionary<string, int>();
            //pessoas.Add("Pedro", 50);
            //pessoas.Add("Maria", 40);
            //pessoas.Add("Thiago", 30);
            //pessoas.Add("José", 40);
            //pessoas.Add("João", 30);
            //pessoas.Add("Roberta", 50);

            //// 2. Cria a consulta
            //var pessoasPorIdade = from p in pessoas
            //                      group p by p.Value;

            //// 3. Recupera o resultado da consulta
            //foreach (var idade in pessoasPorIdade)
            //{
            //    Console.WriteLine($"Pessoas com {idade.Key} anos: ");
            //    foreach (var pessoa in idade)
            //    {
            //        Console.WriteLine(pessoa.Key);
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA COM JOINS
            /////* ********* CONSULTA COM JOINS ******** */
            //Console.WriteLine("********* 5. CONSULTA COM JOINS ********");

            //// 1. Obtém a fonte de dados
            //Dictionary<int, string> clientes = new Dictionary<int, string>();
            //clientes.Add(1, "Pedro");
            //clientes.Add(2, "Maria");
            //clientes.Add(3, "Thiago");
            //clientes.Add(4, "José");
            //clientes.Add(5, "João");
            //clientes.Add(6, "Roberta");

            //Dictionary<string, int> pedidos = new Dictionary<string, int>();
            //pedidos.Add(Guid.NewGuid().ToString(), 1);
            //pedidos.Add(Guid.NewGuid().ToString(), 5);
            //pedidos.Add(Guid.NewGuid().ToString(), 5);
            //pedidos.Add(Guid.NewGuid().ToString(), 3);
            //pedidos.Add(Guid.NewGuid().ToString(), 2);
            //pedidos.Add(Guid.NewGuid().ToString(), 1);
            //pedidos.Add(Guid.NewGuid().ToString(), 2);
            //pedidos.Add(Guid.NewGuid().ToString(), 4);
            //pedidos.Add(Guid.NewGuid().ToString(), 4);
            //pedidos.Add(Guid.NewGuid().ToString(), 6);

            //// 2. Cria a consulta
            //var result = from c in clientes
            //             join p in pedidos on c.Key equals p.Value
            //             select new
            //             {
            //                 Cliente = c.Value,
            //                 Pedido = p.Key
            //             };

            //// 3. Recupera o resultado da consulta
            //foreach(var item in result)
            //{
            //    Console.WriteLine($"Cliente: {item.Cliente} - \t Pedido: {item.Pedido}");
            //}
            #endregion

            #region SINTAXE DE MÉTODOS (Method Syntax)
            int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var query = from n in numeros
                        where (n % 2) == 0
                        select n;

            var method = numeros.Where(n => n % 2 == 0);

            Console.WriteLine("Filtro - Sintaxe de Consulta");
            ExibeResultado(query);

            Console.WriteLine("Filtro - Sintaxe de Método");
            ExibeResultado(method);

            var query2 = from n in numeros
                         where (n % 2) == 0
                         orderby n descending
                         select n;

            var method2 = numeros.Where(n => n % 2 == 0).OrderByDescending(n => n);

            Console.WriteLine();
            Console.WriteLine("Filtro/OrderBy - Sintaxe de Consulta");
            ExibeResultado(query2);

            Console.WriteLine("Filtro/OrderBy - Sintaxe de Método");
            ExibeResultado(method2);
            #endregion

            Console.ReadKey();
        }

        #region AUXILIAR PARA SINTAXE DE MÉTODOS (Method Syntax)
        static void ExibeResultado(IEnumerable<int> items)
        {
            foreach (int item in items)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }
}
