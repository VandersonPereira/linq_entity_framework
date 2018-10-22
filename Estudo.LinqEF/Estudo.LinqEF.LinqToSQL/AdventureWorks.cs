using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Estudo.LinqEF.LinqToSQL
{
    public class AdventureWorks : DataContext
    {
        public AdventureWorks(string connection) : base(connection) { }

        public Table<CodeFirst.ProductCategory> ProductCategories
        {
            get { return this.GetTable<CodeFirst.ProductCategory>(); }
        }

        public Table<CodeFirst.ProductSubcategory> ProductSubcategories
        {
            get { return this.GetTable<CodeFirst.ProductSubcategory>(); }
        }

        // O parâmetro IsComposable é utilizado para informar se a função está mapeada para uma Stored Procedure (false) ou uma função (true).
        [Function(Name = "usp_GetCategory", IsComposable = false)]
        // Mesmo a Stored Procedure retornando só um tipo de entidade por vez, pode ser diferente em cada chamada. Então é necessário informar quais são os tipos de dados retornados:
        [ResultType(typeof(CodeFirst.ProductCategory))]
        [ResultType(typeof(CodeFirst.ProductSubcategory))]
        public IMultipleResults GetCategory(int resultType)
        {
            IExecuteResult executeResult = ExecuteMethodCall(this, (MethodInfo)(MethodInfo.GetCurrentMethod()), resultType);
            IMultipleResults result = (IMultipleResults)executeResult.ReturnValue;
            return result;
        }

        // Por ser Function, o parâmetro isComposable deve ser true.
        // Como ela irá retornar dados de uma tabela, apenas sua entidade é informada. E por retornar apenas um tipo de valor, seu retorno é do tipo IQueryable.
        // Mesmo a Function retornando o resultado de uma pesquisa, não é possível utilizar o tipo ISingleResult.Esse tipo só pode ser aplicado às Stored Procedures.
        // Por usar um nome de parâmetro diferente da função, o atributo Parameter deve ser utilizado, informando o nome do parâmetro na função.
        // Dentro do método praticamente é tudo igual ao explicado para a procedure, com a diferença de o retorno ser convertido para um IQueryable:
        [Function(Name = "ufnFindProductSubcategoriesByName", IsComposable = true)]
        [ResultType(typeof(CodeFirst.ProductSubcategory))]
        public IQueryable<CodeFirst.ProductSubcategory> FindSubcategories([Parameter(Name = "name")] string category)
        {
            IExecuteResult executeResult = ExecuteMethodCall(this, (MethodInfo)(MethodInfo.GetCurrentMethod()), category);
            IQueryable<CodeFirst.ProductSubcategory> result = (IQueryable<CodeFirst.ProductSubcategory>)executeResult.ReturnValue;
            return result;
        }

        // Como a Function retorna um escalar, não é necessário informar o tipo de retorno para o método; basta informar o tipo primitivo que a Function irá retornar.
        // Como esse retorno pode ser nulo, é importante que o tipo de retorno seja informado com “?”, indicando que o tipo pode ser nulo.
        [Function(Name = "ufnGetMaxPriceBySubcategory", IsComposable = true)]
        public decimal? MaxPriceBySubcategory(int productSubcategoryID)
        {
            IExecuteResult executeResult = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), productSubcategoryID);
            decimal? result = (decimal?)executeResult.ReturnValue;
            return result;
        }
    }
}
