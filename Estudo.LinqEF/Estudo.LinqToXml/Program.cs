using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Estudo.LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cria arquivo XML de modo convencional
            CriarXmlDOM();

            // Cria arquivo xml com Linq
            CriarXmlLinq();

            XDocument doc = XDocument.Load(@"C:\Curso\C#\Linq\Contatos2.xml");

            #region Lendo o XML e mostrando na tela
            IEnumerable<XElement> contatos = from c in doc.Descendants("Contato")
                                             where c.Element("Endereco").Element("Bairro").Value == "Bairro XXX"
                                             select c;

            //Em sintaxe de método ficaria da seguinte forma:
            //IEnumerable<XElement> contatos = doc.Descendants("Contato").Where(c => c.Element("Endereco").Element("Bairro").Value == "Bairro XXX");

            foreach (XElement contato in contatos)
            {
                Console.WriteLine("{0}", contato.Element("Nome").Value);

                Console.WriteLine("\nTelefones:");

                XElement telefones = contato.Element("Telefones");
                foreach (XElement telefone in telefones.Elements("Telefone"))
                {
                    string tipo = telefone.Attribute("Tipo").Value;
                    string numero = telefone.Value;

                    Console.WriteLine("{0}: {1}", tipo, numero);
                }

                Console.WriteLine("\nEndereço:");

                XElement endereco = contato.Element("Endereco");
                Console.WriteLine("{0}", endereco.Element("Logradouro").Value);
                Console.WriteLine("Bairro: {0}", endereco.Element("Bairro").Value);
                Console.WriteLine("Cidade: {0}", endereco.Element("Cidade").Value);
                Console.WriteLine("Estado: {0}", endereco.Element("Estado").Value);
                Console.WriteLine("CEP: {0}", endereco.Element("CEP").Value);
            }
            #endregion

            #region Passando do XML para um objeto
            List<Contato> contatosDoc2 = (from c in doc.Descendants("Contato")
                                      select new Contato
                                      {
                                          Nome = c.Element("Nome").Value,
                                          Endereco = new Endereco()
                                          {
                                              Logradouro = c.Element("Endereco").Element("Logradouro").Value,
                                              Bairro = c.Element("Endereco").Element("Bairro").Value,
                                              Cidade = c.Element("Endereco").Element("Cidade").Value,
                                              Estado = c.Element("Endereco").Element("Estado").Value,
                                              CEP = c.Element("Endereco").Element("CEP").Value
                                          }
                                      }).ToList();

            //Em sintaxe de método ficaria da seguinte forma:
            //List<Contato> contat = doc.Descendants("Contato")
            //                        .Select(
            //                            c => new Contato
            //                            {
            //                                Nome = c.Element("Nome").Value,
            //                                Endereco = new Endereco()
            //                                {
            //                                    Logradouro = c.Element("Endereco").Element("Logradouro").Value,
            //                                    Bairro = c.Element("Endereco").Element("Bairro").Value,
            //                                    Cidade = c.Element("Endereco").Element("Cidade").Value,
            //                                    Estado = c.Element("Endereco").Element("Estado").Value,
            //                                    CEP = c.Element("Endereco").Element("CEP").Value
            //                                }
            //                            }).ToList();



            foreach (Contato contato in contatosDoc2)
            {
                Console.WriteLine("{0}", contato.Nome);

                Endereco endereco = contato.Endereco;
                Console.WriteLine("{0}", endereco.Logradouro);
                Console.WriteLine("Bairro: {0}", endereco.Bairro);
                Console.WriteLine("Cidade: {0}", endereco.Cidade);
                Console.WriteLine("Estado: {0}", endereco.Estado);
                Console.WriteLine("CEP: {0}", endereco.CEP);

                Console.WriteLine();
            }
            #endregion

            #region Passando do objeto para o XML
            List<Contato> objctContatos = CriarContatos();

            XElement xmlContatos = new XElement("Contatos",
            from c in objctContatos
            select new XElement("Contato",
            new XElement("Nome", c.Nome),
                new XElement("Endereco",
                    new XElement("Logradouro", c.Endereco.Logradouro),
                    new XElement("Bairro", c.Endereco.Bairro),
                    new XElement("Cidade", c.Endereco.Cidade),
                    new XElement("Estado", c.Endereco.Estado),
                    new XElement("CEP", c.Endereco.CEP))
                ));

            //Em sintaxe de método ficaria da seguinte forma:
            //XElement xmlContatos2 = new XElement("Contatos", contatos.Select(c => new XElement("Contato",
            //                                                            new XElement("Nome", c.Nome),
            //                                                               new XElement("Endereco",
            //                                                                   new XElement("Logradouro", c.Endereco.Logradouro),
            //                                                                   new XElement("Bairro", c.Endereco.Bairro),
            //                                                                   new XElement("Cidade", c.Endereco.Cidade),
            //                                                                   new XElement("Estado", c.Endereco.Estado),
            //                                                                   new XElement("CEP", c.Endereco.CEP))
            //                                                               )));

            xmlContatos.Save(@"C:\Curso\C#\Linq\Contatos3.xml");
            Console.WriteLine(@"Criando o arquivo XML com DOM em C:\Curso\C#\Linq\Contatos3.xml");
            #endregion

            Console.ReadKey();
        }

        static void CriarXmlDOM()
        {
            // Cria o XmlDocument
            XmlDocument contactsDocument = new XmlDocument();

            // Define a instrução de processamento do arquivo e o elemento raiz
            contactsDocument.AppendChild(contactsDocument.CreateProcessingInstruction
            ("xml", "version='1.0' encoding='UTF-8' standalone='yes'"));
            contactsDocument.AppendChild(contactsDocument.CreateElement("Contatos"));

            //Cria elemento filho do elemento raiz e o adiciona à raiz
            XmlElement contactElement = contactsDocument.CreateElement("Contato");
            contactsDocument.DocumentElement.AppendChild(contactElement);

            //Cria o elemento filho do elemento Contato, atribui um valor e o adiciona ao elemento
            XmlElement nameElement = contactsDocument.CreateElement("Nome");
            nameElement.InnerText = "Thiago Mônaco";
            contactElement.AppendChild(nameElement);

            //Cria elemento filho do elemento Contato e o adiciona ao elemento
            XmlElement phonesElement = contactsDocument.CreateElement("Telefones");
            contactElement.AppendChild(phonesElement);

            //Cria elemento filho do elemento Telefones, atribui um valor, um atributo e o adiciona  ao elemento
            XmlElement phoneElement = contactsDocument.CreateElement("Telefone");
            phoneElement.InnerText = "(11) 9999-9999";
            phoneElement.SetAttribute("Tipo", "Celular");
            phonesElement.AppendChild(phoneElement);

            //Cria elemento filho do elemento Telefones, atribui um valor, um atributo e o adiciona  ao elemento
            phoneElement = contactsDocument.CreateElement("Telefone");
            phoneElement.InnerText = "(11) 5555-5555";
            phoneElement.SetAttribute("Tipo", "Residencial");
            phonesElement.AppendChild(phoneElement);

            //Cria elemento filho do elemento Contato e o adiciona ao elemento
            XmlElement adressElement = contactsDocument.CreateElement("Endereco");
            contactElement.AppendChild(adressElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            XmlElement streetElement = contactsDocument.CreateElement("Logradouro");
            streetElement.InnerText = "R: Xpto, 1234";
            adressElement.AppendChild(streetElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            XmlElement districtElement = contactsDocument.CreateElement("Bairro");
            districtElement.InnerText = "Bairro XXX";
            adressElement.AppendChild(districtElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            XmlElement cityElement = contactsDocument.CreateElement("Cidade");
            cityElement.InnerText = "São Paulo";
            adressElement.AppendChild(cityElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            XmlElement stateElement = contactsDocument.CreateElement("Estado");
            stateElement.InnerText = "SP";
            adressElement.AppendChild(stateElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            XmlElement zipCodeElement = contactsDocument.CreateElement("CEP");
            zipCodeElement.InnerText = "01010-010";
            adressElement.AppendChild(zipCodeElement);

            //Adiciona outro elemento Contato ao XML:

            contactElement = contactsDocument.CreateElement("Contato");
            contactsDocument.DocumentElement.AppendChild(contactElement);

            //Cria elemento filho do elemento Contato, atribui um valor e o adiciona ao elemento
            nameElement = contactsDocument.CreateElement("Nome");
            nameElement.InnerText = "TPaulo Pedro";
            contactElement.AppendChild(nameElement);

            //Cria elemento filho do elemento Contato e o adiciona ao elemento
            phonesElement = contactsDocument.CreateElement("Telefones");
            contactElement.AppendChild(phonesElement);

            //Cria elemento filho do elemento Telefones, atribui um valor, um atributo e o adiciona  ao elemento
            phoneElement = contactsDocument.CreateElement("Telefone");
            phoneElement.InnerText = "(11) 8888-8888";
            phoneElement.SetAttribute("Tipo", "Celular");
            phonesElement.AppendChild(phoneElement);

            //Cria elemento filho do elemento Telefones, atribui um valor, um atributo e o adiciona  ao elemento
            phoneElement = contactsDocument.CreateElement("Telefone");
            phoneElement.InnerText = "(11) 8888-8888";
            phoneElement.SetAttribute("Tipo", "Residencial");
            phonesElement.AppendChild(phoneElement);

            //Cria elemento filho do elemento Contato e o adiciona ao elemento
            adressElement = contactsDocument.CreateElement("Endereco");
            contactElement.AppendChild(adressElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            streetElement = contactsDocument.CreateElement("Logradouro");
            streetElement.InnerText = "R: ABC, 1234";
            adressElement.AppendChild(streetElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            districtElement = contactsDocument.CreateElement("Bairro");
            districtElement.InnerText = "Bairro AAA";
            adressElement.AppendChild(districtElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            cityElement = contactsDocument.CreateElement("Cidade");
            cityElement.InnerText = "São Paulo";
            adressElement.AppendChild(cityElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            stateElement = contactsDocument.CreateElement("Estado");
            stateElement.InnerText = "SP";
            adressElement.AppendChild(stateElement);

            //Cria elemento filho do elemento Endereco, atribui um valor e o adiciona ao elemento
            zipCodeElement = contactsDocument.CreateElement("CEP");
            zipCodeElement.InnerText = "02020-020";
            adressElement.AppendChild(zipCodeElement);

            contactsDocument.Save(@"C:\Curso\C#\Linq\Contatos1.xml");
            Console.WriteLine(@"Criando o arquivo XML com DOM em C:\Curso\C#\Linq\Contatos1.xml");
        }

        static void CriarXmlLinq()
        {
            XElement contatos =
                new XElement("Contatos",
                    new XElement("Contato",
                        new XElement("Nome", "Thiago Mônaco"),
                        new XElement("Telefones",
                            new XElement("Telefone", "(11) 9999-9999",
                                new XAttribute("Tipo", "Celular")),
                            new XElement("Telefone", "(11) 5555-5555",
                                new XAttribute("Tipo", "Residencial"))
                        ),
                        new XElement("Endereco",
                            new XElement("Logradouro", "R: Xpto, 1234"),
                            new XElement("Bairro", "Bairro XXX"),
                            new XElement("Cidade", "São Paulo"),
                            new XElement("Estado", "SP"),
                            new XElement("CEP", "01010-010")
                        )
                    ),
                    new XElement("Contato",
                        new XElement("Nome", "Paulo Pedro"),
                        new XElement("Telefones",
                            new XElement("Telefone", "(11) 8888-8888",
                                new XAttribute("Tipo", "Celular")),
                            new XElement("Telefone", "(11) 4444-4444",
                                new XAttribute("Tipo", "Residencial"))
                        ),
                        new XElement("Endereco",
                            new XElement("Logradouro", "R: ABC, 1234"),
                            new XElement("Bairro", "Bairro AAA"),
                            new XElement("Cidade", "São Paulo"),
                            new XElement("Estado", "SP"),
                            new XElement("CEP", "02020-020")
                        )
                    )
                );

            contatos.Save(@"C:\Curso\C#\Linq\Contatos2.xml");
            Console.WriteLine(@"Criando o arquivo XML com LINQ to XML em C:\Curso\C#\Linq\Contatos2.xml");
        }

        static List<Contato> CriarContatos()
        {
            List<Contato> contatos = new List<Contato>();

            contatos.Add(
                            new Contato()
                            {
                                Nome = "Felipe Mourato",
                                Endereco = new Endereco()
                                {
                                    Logradouro = "R: ABC, 1234",
                                    Bairro = "Bairro AAA",
                                    Cidade = "São Paulo",
                                    Estado = "SP",
                                    CEP = "02020-020"
                                }
                            }
                        );

            contatos.Add(
                            new Contato()
                            {
                                Nome = "Mauro Mauricio",
                                Endereco = new Endereco()
                                {
                                    Logradouro = "R: GHF, 0893",
                                    Bairro = "Bairro CCC",
                                    Cidade = "São Paulo",
                                    Estado = "SP",
                                    CEP = "04040-040"
                                }
                            }
                        );

            contatos.Add(
                            new Contato()
                            {
                                Nome = "Suzan Suzi",
                                Endereco = new Endereco()
                                {
                                    Logradouro = "R: DEF, 567",
                                    Bairro = "Bairro BBB",
                                    Cidade = "Betin",
                                    Estado = "MG",
                                    CEP = "03030-030"
                                }
                            }
                        );

            contatos.Add(
                            new Contato()
                            {
                                Nome = "Claudia Rodrigues",
                                Endereco = new Endereco()
                                {
                                    Logradouro = "R: JKL, 890",
                                    Bairro = "Bairro DDD",
                                    Cidade = "Rio de Janeiro",
                                    Estado = "RJ",
                                    CEP = "06060-060"
                                }
                            }
                        );

            return contatos;

        }
    }
}
