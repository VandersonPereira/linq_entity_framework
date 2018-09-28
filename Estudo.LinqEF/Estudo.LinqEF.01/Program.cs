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
            /* ********* CONSULTA SIMPLES, SEM WHERE ******** */
            Console.WriteLine("********* 1. CONSULTA SIMPLES, SEM WHERE ********");

            // 1. Obtém a fonte de dados
            int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // 2. Cria a consulta
            var querySemWhere = from num in numeros
                                select num;

            // 3. Recupera o resultado da consulta
            foreach (var numero in querySemWhere)
                Console.WriteLine(numero);

            Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE
            /* ********* CONSULTA SIMPLES, COM WHERE ******** */
            Console.WriteLine("********* 2. CONSULTA SIMPLES, COM WHERE ********");

            // 2. Cria a consulta
            var queryComWhere = from num in numeros
                                where (num % 2) == 0 // Pode-se usar operadores && e ||
                                select num;

            // 3. Recupera o resultado da consulta
            foreach (var numero in queryComWhere)
                Console.WriteLine(numero);

            Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE E ORDER BY 
            /* ********* CONSULTA SIMPLES, COM WHERE E ORDER BY ******** */
            Console.WriteLine("********* 3. CONSULTA SIMPLES, COM WHERE E ORDER BY ********");

            // 2. Cria a consulta
            var queryComWhereComOrderBy = from num in numeros
                                where (num % 2) == 0 // Pode-se usar operadores && e ||
                                orderby num descending
                                select num;

            // 3. Recupera o resultado da consulta
            foreach (var numero in queryComWhereComOrderBy)
                Console.WriteLine(numero);

            Console.WriteLine("-----------------------------------------------------");
            #endregion

            #region CONSULTA SIMPLES, COM WHERE E GROUP BY
            /* ********* CONSULTA SIMPLES, COM WHERE E GROUP BY ******** */
            Console.WriteLine("********* 4. CONSULTA SIMPLES, COM WHERE E GROUP BY ********");


            // 1. Obtém a fonte de dados
            Dictionary<string, int> pessoas = new Dictionary<string, int>();
            pessoas.Add("Pedro", 50);
            pessoas.Add("Maria", 40);
            pessoas.Add("Thiago", 30);
            pessoas.Add("José", 40);
            pessoas.Add("João", 30);
            pessoas.Add("Roberta", 50);

            // 2. Cria a consulta
            var pessoasPorIdade = from p in pessoas
                                  group p by p.Value;
        
            // 3. Recupera o resultado da consulta
            foreach (var idade in pessoasPorIdade)
            {
                Console.WriteLine($"Pessoas com {idade.Key} anos: ");
                foreach (var pessoa in idade)
                {
                    Console.WriteLine(pessoa.Key);
                }
                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------------------------------");
            #endregion


            Console.ReadKey();
        }
    }
}
