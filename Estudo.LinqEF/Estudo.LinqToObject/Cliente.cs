using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToObject
{
    public class Cliente
    {
        public enum SexoEnum
        {
            Masculino,
            Feminino
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public SexoEnum Sexo { get; set; }
        public string Email { get; set; }
    }
}
