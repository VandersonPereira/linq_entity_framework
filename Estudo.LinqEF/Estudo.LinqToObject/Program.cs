using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cliente> clientes = CriarCliente();
            List<Pedido> pedidos = CriarPedidos();

            #region Lidando com os clientes
            IEnumerable<Cliente> resultado;

            resultado = from c in clientes
                        orderby c.Nome descending
                        select c;

            // Em sintaxe de método, ficaria assim:
            // resultado = clientes.OrderByDescending(c => c.Nome);

            Console.WriteLine("Ordenação:");
            ExibirResultado(resultado.ToList());

            resultado = from c in clientes
                        where c.Nome == "Cliente 3"
                        select c;

            // Em sintaxe de método, ficaria assim:
            // resultado = clientes.Where(c => c.Nome.Equals("Cliente 3"));
            // ou para usar o 'Contains'
            // resultado = clientes.Where(c => c.Email.Contains("cliente3"));

            Console.WriteLine("Filtro:");
            ExibirResultado(resultado.ToList());
            #endregion

            #region Lidando com os pedidos
            // Join
            var result = from c in clientes
                         join p in pedidos
                         on c.ID equals p.IdCliente
                         into ClientesComPedido
                         select new
                         {
                             c.Nome,
                             TotalDePedidos = ClientesComPedido.Count()
                         };

            // Em sintaxe de método, ficaria assim:
            /*var result = clientes.Join(pedidos, c => c.ID, p => p.IdCliente, (cliente, pedido) => new { cliente, pedido })
                            .GroupBy(a => a.cliente.Nome)
                            .Select(g => new { Nome = g.Key, TotalPedidos = g.Count() });
            */

            foreach (var item in result)
            {
                Console.WriteLine("******** PEDIDOS POR CLIENTE *********");
                Console.WriteLine(item);
                Console.WriteLine("**************************************");
            }

            // Sum
            var resultSum = from c in clientes
                            join p in pedidos
                            on c.ID equals p.IdCliente
                            into ClientesComPedido
                            select new
                            {
                                c.Nome,
                                TotalValorPedidos = ClientesComPedido.Sum(p => p.Preco)
                            };

            foreach (var item in resultSum)
            {
                Console.WriteLine("******** PEDIDOS POR CLIENTE (Valor) *********");
                Console.WriteLine(item);
                Console.WriteLine("**************************************");
            }

            #endregion

            #region Listando arquivos em uma pasta
            string diretorio = System.Environment.SystemDirectory;
            Console.WriteLine("");
            Console.WriteLine("***********************************************");
            Console.WriteLine(diretorio);

            DirectoryInfo dirInfo = new DirectoryInfo(diretorio);

            var arquivos = dirInfo.GetFiles("*.*").ToList();

            var agrupado = from a in arquivos
                           group a by a.Extension
                           into fgroup
                           orderby fgroup.Key
                           select fgroup;

            // Em sintaxe de método, ficaria assim:
            // var agrupado = arquivos.GroupBy(a => a.Extension).OrderBy(a => a.Key);

            foreach (var fileGroup in agrupado)
            {
                Console.WriteLine($"{fileGroup.Key} - {fileGroup.Count()} arquivos");

                foreach (var file in fileGroup)
                {
                    Console.WriteLine(file.Name);
                }
                Console.WriteLine("");
            }

            #endregion

            Console.ReadKey();

        }

        public static List<Cliente> CriarCliente()
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(
                            new Cliente
                            {
                                ID = 1,
                                Nome = "Cliente 1",
                                Sexo = Cliente.SexoEnum.Feminino,
                                DataNascimento = new DateTime(1980,1,1),
                                Email = "cliente1@email.com"
                            }
                        );

            clientes.Add(
                            new Cliente
                            {
                                ID = 2,
                                Nome = "Cliente 2",
                                Sexo = Cliente.SexoEnum.Feminino,
                                DataNascimento = new DateTime(1999, 3, 24),
                                Email = "cliente2@email.com"
                            }
                        );

            clientes.Add(
                            new Cliente
                            {
                                ID = 3,
                                Nome = "Cliente 3",
                                Sexo = Cliente.SexoEnum.Masculino,
                                DataNascimento = new DateTime(1985, 9, 22),
                                Email = "cliente3@email.com"
                            }
                        );

            clientes.Add(
                            new Cliente
                            {
                                ID = 4,
                                Nome = "Cliente 4",
                                Sexo = Cliente.SexoEnum.Masculino,
                                DataNascimento = new DateTime(1990, 12, 13),
                                Email = "cliente4@email.com"
                            }
                        );

            return clientes;
        }

        public static void ExibirResultado(List<Cliente> clientes)
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine("*************************");
                Console.WriteLine($"{cliente.ID} - {cliente.Nome}");
                Console.WriteLine($"Nascimento: {cliente.DataNascimento}");
                Console.WriteLine($"Sexo: {cliente.Sexo}");
                Console.WriteLine($"E-mail: {cliente.Email}");
                Console.WriteLine("*************************");
                Console.WriteLine("");
            }
        }

        public static List<Pedido> CriarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            pedidos.Add(
                            new Pedido
                            {
                                ID = 1,
                                IdCliente = 1,
                                Data = DateTime.Now,
                                Preco = 40.00
                            }
                        );

            pedidos.Add(
                            new Pedido
                            {
                                ID = 2,
                                IdCliente = 1,
                                Data = DateTime.Now,
                                Preco = 100.90
                            }
                        );

            pedidos.Add(
                            new Pedido
                            {
                                ID = 3,
                                IdCliente = 2,
                                Data = DateTime.Now,
                                Preco = 450.00
                            }
                        );

            pedidos.Add(
                            new Pedido
                            {
                                ID = 4,
                                IdCliente = 3,
                                Data = DateTime.Now,
                                Preco = 32.10
                            }
                        );

            pedidos.Add(
                            new Pedido
                            {
                                ID = 5,
                                IdCliente = 3,
                                Data = DateTime.Now,
                                Preco = 343.52
                            }
                        );

            pedidos.Add(
                            new Pedido
                            {
                                ID = 6,
                                IdCliente = 4,
                                Data = DateTime.Now,
                                Preco = 134.98
                            }
                        );

            return pedidos;
        }
    }
}
