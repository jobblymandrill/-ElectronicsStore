drop proc if exists dbo.Product_Search
go
create proc [dbo].[Product_Search]	 

		@Id bigint = null,
		@IdOperation nvarchar(30) = null,
		@Name nvarchar(100) = null,
		@NameOperation nvarchar(30) = null,
		@Price money = null,
		@PriceOperation nvarchar(30) = null,
		@TradeMark nvarchar(100) = null,
		@TradeMarkOperation nvarchar(30) = null,
		@CategoryId int = null,
		@CategoryIdOperation nvarchar(30) = null,
		@ParentCategoryId int = null,
		@ParentCategoryIdOperation nvarchar(30) = null,
		@CategoryName nvarchar(100) = null,
		@CategoryNameOperation nvarchar(30) = null,
		@ParentCategoryName nvarchar(100) = null,
		@ParentCategoryNameOperation nvarchar(30) = null,
		@PriceEnd money = null
 
as 
begin 

declare 

@sql nvarchar(max)

set @sql = N'select	
		p.Id,
		p.Name,
		p.Price,
		p.TradeMark,
		p.CategoryId,
		p.ParentCategoryId,
		c.Name,
		cd.Name
		
		from dbo.[Product] p
		inner join dbo.Category c on c.Id = p.CategoryId
		inner join dbo.Category cd on cd.Id = p.ParentCategoryId
		where '

	if @Id is not null and @IdOperation = 1
	set @sql=@sql+N'p.Id=' + CAST (@Id as nvarchar )+ ' and ' 

	if @Id is not null and @IdOperation = 2
	set @sql=@sql+N'p.Id>' + CAST (@Id as nvarchar )+ ' and ' 

	if @Id is not null and @IdOperation = 3
	set @sql=@sql+N'p.Id<' + CAST (@Id as nvarchar )+ ' and ' 
	
	if @Name is not null and @NameOperation = 1
	set @sql=@sql+N'p.Name =''' + @Name + ''' and ' 

	if @Name is not null and @NameOperation = 2
	set @sql=@sql+N'p.Name like''' + @Name + ''' and ' 

	if @Price is not null and @PriceEnd is null and @PriceOperation = 1
	set @sql=@sql+N'p.Price='''+CAST (@Price as nvarchar)+ ''' and '
	
	if @Price is not null and @PriceEnd is not null and @PriceOperation = 1
	set @sql=@sql+N'p.Price >''' + CAST (@Price as nvarchar) + ''' and p.Price <''' + CAST (@PriceEnd as nvarchar) + ''' and '

	if @Price is not null and @PriceEnd is null and @PriceOperation = 2
	set @sql=@sql+N'p.Price>'''+CAST (@Price as nvarchar)+ ''' and '

	if @Price is not null and @PriceEnd is null and @PriceOperation = 3
	set @sql=@sql+N'p.Price<'''+CAST (@Price as nvarchar)+ ''' and '

	if @TradeMark is not null  and @TradeMarkOperation = 1
	set @sql=@sql+N'p.TradeMark =''' + @TradeMark + ''' and ' 

	if @TradeMark is not null  and @TradeMarkOperation = 2
	set @sql=@sql+N'p.TradeMark like''' + @TradeMark + ''' and ' 

	if @CategoryId is not null
	set @sql=@sql+N'p.CategoryId =''' + cast (@CategoryId as nvarchar) + ''' and ' 

    if @ParentCategoryId is not null
	set @sql=@sql+N'p.ParentCategoryId =''' + cast (@ParentCategoryId as nvarchar) + ''' and '

	if @CategoryName is not null and @CategoryIdOperation = 1
	set @sql=@sql+N'c.Name=''' + @CategoryName + ''' and '

	if @CategoryName is not null and @CategoryIdOperation = 2
	set @sql=@sql+N'c.Name like ''' + @CategoryName + ''' and '

	if @ParentCategoryName is not null and @ParentCategoryNameOperation = 1
	set @sql=@sql+N'cd.Name=''' + @ParentCategoryName + ''' and '

	if @ParentCategoryName is not null and @ParentCategoryNameOperation = 2
	set @sql=@sql+N'cd.Name like ''' + @ParentCategoryName + ''' and '

	set @sql=@sql+N'1 = 1'

	print @sql
	EXECUTE sp_executesql @SQL

end 

-- exec [dbo].[Product_Search] 1226223,1
-- exec [dbo].[Product_Search] 1226223,2
-- exec [dbo].[Product_Search] 1226223,3
-- exec [dbo].[Product_Search] null, null, 'Ноутбук Apple MacBook Pro 13', 1
-- exec [dbo].[Product_Search] null, null, 'Ноутбук Apple MacBook Pro 13', 2
-- exec [dbo].[Product_Search] null, null, null, null, '92990.00', 1, null, null, null, null, null, null, null, null, null, null, null
-- exec [dbo].[Product_Search] null, null, null, null, '92990.00', 1, null, null, null, null, null, null, null, null, null, null, '94990.00'
-- exec [dbo].[Product_Search] null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'Ноутбуки', 2, null
