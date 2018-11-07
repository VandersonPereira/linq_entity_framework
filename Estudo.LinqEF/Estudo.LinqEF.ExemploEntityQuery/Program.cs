using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntityQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BancoCodeFirst dc = new BancoCodeFirst())
            {
                #region Exemplos simples
                // Consulta simples
                foreach (var item in dc.Funcionarios)
                {
                    Console.WriteLine(item.Nome);
                }

                Console.WriteLine("\n-------------------------\n");
                
                // Consulta com mais opções
                var result = from fun in dc.Funcionarios
                             orderby fun.Nome
                             select fun;

                //Em sintaxe de método ficaria da seguinte forma:
                //var result = dc.Funcionarios.OrderBy(fun => fun.Nome);

                // Recuperando a sintax SQL da consulta
                Console.WriteLine("SQL: {0} \n", result.ToString());

                foreach (var item in result)
                {
                    Console.WriteLine(item.Nome);
                }

                Console.WriteLine("\n-------------------------\n");

                // Mais consultas
                var resultFiltroSalario = from fun in dc.Funcionarios
                             where fun.Salario > 2000M
                             orderby fun.Nome
                             select fun;

                //Em sintaxe de método ficaria da seguinte forma:
                //var result = dc.Funcionarios.Where(fun => fun.Salario > 2000M).OrderBy(fun => fun.Nome);

                Console.WriteLine("SQL: \n{0} \n", resultFiltroSalario.ToString());

                foreach (var item in resultFiltroSalario)
                {
                    Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                }
                #endregion

                Console.WriteLine("\n-------------------------\n");

                #region Salvando dados em memória
                // Note que, no início do código, foi pega uma lista com os funcionários cadastrados.
                // Será apenas nesse momento que código irá acessar a base de dados.
                var funcionarios = dc.Funcionarios.ToList();

                var resultMemoria = from fun in funcionarios
                             where fun.Salario > 2000M
                             orderby fun.Nome
                             select fun;

                //Em sintaxe de método ficaria da seguinte forma:
                //var result = funcionarios.Where(fun => fun.Salario > 2000M).OrderBy(fun => fun.Nome);

                Console.WriteLine("Funcionários com salários acima de R$ 2000");
                foreach (var item in resultMemoria)
                {
                    Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                }

                Console.WriteLine("\n");

                var resultMemoria2 = from fun in funcionarios
                              where fun.Salario <= 2000M
                              orderby fun.Nome
                              select fun;

                //Em sintaxe de método ficaria da seguinte forma:
                //var result2 = funcionarios.Where(fun => fun.Salario <= 2000M).OrderBy(fun => fun.Nome);

                Console.WriteLine("Funcionários com salários abaixo de R$ 2000");
                foreach (var item in resultMemoria2)
                {
                    Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                }

                #endregion

                Console.WriteLine("\n-------------------------\n");

                #region Tipo de carregamentos
                // Lazy
                var queryLazy = from d in dc.Departamentos
                            where d.Nome == "Desenvolvimento"
                            select d;

                //Em sintaxe de método ficaria da seguinte forma:
                //var query = dc.Departamentos.Where(d => d.Nome == "Desenvolvimento");

                var dev = queryLazy.Single();

                Console.WriteLine("Funcionários do departamento {0}:", dev.Nome);

                foreach (var item in dev.Funcionarios)
                {
                    Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                }

                /*
                O carregamento lazy é útil, mas é necessário tomar cuidado ao utilizá-lo,
                porque ele efetua nova consulta no banco de dados quando a propriedade de navegação é utilizada. 
                Então, dependendo da forma que for aplicado, pode ser mais viável utilizar as outras formas de carregamento. 
                Um ponto importante: o carregamento lazy só funciona dentro do contexto do DbContext (no caso do exemplo anterior, seria apenas dentro do using). 
                */

                Console.WriteLine("\n-------------------------\n");

                // Eager
                var queryEager = from d in dc.Departamentos.Include("Funcionarios")
                             orderby d.Nome
                             select d;

                //Em sintaxe de método ficaria da seguinte forma:
                //var result = dc.Departamentos.Include("Funcionarios").OrderBy(d => d.Nome);

                foreach (var departamento in queryEager)
                {
                    Console.WriteLine("Funcionários do departamento {0}:", departamento.Nome);

                    foreach (var item in departamento.Funcionarios)
                    {
                        Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                    }
                    Console.WriteLine();
                }

                // Note que o carregamento eager é ativado com o método Include.
                // Esse método deve receber o nome da propriedade de navegação que será carregada na pesquisa.Se a propriedade não existir, dará erro durante a execução.
                // Nesse método também é possível informar a propriedade de navegação da outra entidade para ser carregada durante a consulta.Por exemplo:
                dc.Departamentos.Include("Funcionarios.Departamento");

                Console.WriteLine("\n-------------------------\n");

                // Explicit
                // Para esse tipo de carregamento, não se faz necessário o tipo 'virtual' nas propriedades das entidades.
                // Após remove-los, pode cosultar usando esse exemplo a seguir:
                /*
                var query = from d in dc.Departamentos
                    where d.Nome == "Desenvolvimento"
                    select d;

                //Em sintaxe de método ficaria da seguinte forma:
                //var query = dc.Departamentos.Where(d => d.Nome == "Desenvolvimento");

                var dev = query.Single();

                dc.Entry(dev).Collection("Funcionarios").Load();

                Console.WriteLine("Funcionários do departamento {0}:", dev.Nome);

                foreach (var item in dev.Funcionarios)
                {
                    Console.WriteLine("{0}, \t salário {1}", item.Nome, item.Salario.ToString("C"));
                }
                */
                #endregion

            }

            Console.ReadKey();
        }
    }
}
