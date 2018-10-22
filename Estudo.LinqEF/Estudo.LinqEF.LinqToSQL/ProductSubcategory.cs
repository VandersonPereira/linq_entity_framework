using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToSQL.CodeFirst
{
    [Table(Name = "Production.ProductSubcategory")]
    public class ProductSubcategory
    {
        [Column(IsPrimaryKey = true)]
        public int ProductSubcategoryID { get; set; }

        [Column]
        public int ProductCategoryID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(Name = "rowguid")]
        public Guid guid { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }

        private EntityRef<ProductCategory> _ProductCategory;

        [Association(Storage = "_ProductCategory", ThisKey = "ProductCategoryID", IsForeignKey = true)]
        public ProductCategory ProductCategory
        {
            get { return this._ProductCategory.Entity; }
            set { this._ProductCategory.Entity = value; }
        }
    }
}

/*
O atributo Association possui os parâmetros:

    * Name: Define o nome utilizado para estabelecer o relacionamento.
    * Storage: Informa em qual variável, declarada na classe, será armazenado o valor vindo do banco de dados.
    * ThisKey: Informa em qual campo da tabela o valor da chave está armazenado.
    * OtherKey: Informa em qual variável, declarada na classe referenciada, está armazenado o valor da chave-estrangeira.
    * IsForeignKey: Informa que o campo passado em ThisKey é uma chave-estrangeira. 
*/
