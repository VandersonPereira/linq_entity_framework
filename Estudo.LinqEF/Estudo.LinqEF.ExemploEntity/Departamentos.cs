using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.ExemploEntity
{
    public class Departamentos
    {
        [Key]
        public int DepartamentosId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}
