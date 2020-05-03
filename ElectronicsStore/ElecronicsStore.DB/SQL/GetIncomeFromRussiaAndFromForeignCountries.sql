create proc [dbo].[GetIncomeFromRussiaAndFromForeignCountries]
as
begin
SELECT
        [0] AS 'IncomeFromRussia',
        [1] AS 'IncomeFromOutsideRussia'
  FROM (
    SELECT SUM(p.Price*op.Amount) as total,
    c.IsForeign
  FROM
    dbo.Product p
        inner join dbo.Order_Product_Amount op on op.ProductId = p.Id
        inner join dbo.[Order] o on o.Id = op.OrderId
        inner join dbo.Filial c on c.Id = o.FilialId
  WHERE c.IsForeign is not null
  GROUP BY
    c.IsForeign) as s
  PIVOT
  (
  AVG(total)
  FOR s.IsForeign IN ([0], [1])
  ) AS PivotTable;
end;
