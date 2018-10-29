using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estudo.LinqEF.ExemploEntity
{
    [Table("Functionario")]
    public class Funcionario_DataAnotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdFuncionario { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Nome { get; set; }
        [StringLength(500)]
        public string Cargo { get; set; }

        public decimal Salario { get; set; }

        public virtual Departamentos Departamento { get; set; }
    }
}
