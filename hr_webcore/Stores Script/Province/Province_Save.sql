USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Province_Save]    Script Date: 2/27/2019 2:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Province_Save]
@ProvinceID			int,
@CountryID			int,
@Name				nvarchar(100),
@NameEN				nvarchar(100),
@Status				int
AS
BEGIN
DECLARE @Province_Save int
	select @Province_Save = count(*) from Province where ProvinceID = @ProvinceID
	if (@Province_Save = 0)		
		begin
			insert into Province (CountryID,Name,NameEN,Status) values(@CountryID,@Name, @NameEN,@Status)
		end	
	else
		begin
			UPDATE  Province
			   SET 
				  CountryID=@CountryID
				 ,Name = @Name
				 ,NameEN = @NameEN					
				 ,Status = @Status				
				 WHERE ProvinceID = @ProvinceID
		end	
END
