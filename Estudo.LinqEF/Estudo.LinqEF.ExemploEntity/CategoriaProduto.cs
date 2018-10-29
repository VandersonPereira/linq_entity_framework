using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Estudo.LinqEF.ExemploEntity
{
    [Table("ProductCategory", Schema = "Production")]
    public class CategoriaProduto
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SubCategoriaProduto> ProductSubcategories { get; set; }

        public CategoriaProduto()
        {
            this.ProductSubcategories = new HashSet<SubCategoriaProduto>();
        }

    }
}
