using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToSQL.CodeFirst
{
    [Table(Name = "Production.ProductCategory")]
    public class ProductCategory
    {
        [Column(IsPrimaryKey = true)]
        public int ProductCategoryID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(Name = "rowguid")]
        public Guid guid { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }

        [Association(OtherKey = "ProductCategoryID")]
        public EntitySet<ProductSubcategory> ProductSubcategories { get; set; }
    }
}

/*
 Além do parâmetro Name, o atributo Column possui os seguintes:

    * Storage: Informa em qual variável, declarada na classe, será armazenado o valor que vem do banco de dados.
    * AutoSync: Define se o valor será automaticamente sincronizado com o valor gerado pelo banco de dados em comandos de INSERT e UPDATE. Aceita valores da enumeração AutoSync, que são AutoSync.Always, AutoSync.Never, AutoSync.OnInsert e AutoSync.OnUpdate.
    * DbType: Mapeia o tipo de dados definido na coluna do banco de dados.
    * IsPrimaryKey: Informa se a coluna é uma chave-primária.
    * IsDbGenerated: Informa se o valor da coluna é gerado automaticamente pelo banco de dados.
    * CanBeNull: Define se o campo pode, ou não, receber valores nulos. 
*/
