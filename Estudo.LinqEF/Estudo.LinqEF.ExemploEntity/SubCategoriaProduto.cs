using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estudo.LinqEF.ExemploEntity
{
    [Table("ProductSubcategory", Schema = "Prodution")]
    public class SubCategoriaProduto
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual CategoriaProduto ProductCategory { get; set; }
    }
}
