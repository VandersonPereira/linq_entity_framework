CREATE PROCEDURE usp_GetCategory(@resultType INT = 1)
AS BEGIN
  IF @resultType = 1
    SELECT *  FROM Production.ProductCategory
  ELSE IF @resultType = 2
    SELECT *  FROM Production.ProductSubcategory
END