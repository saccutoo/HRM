USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[PersonalIncomeTax_Save]    Script Date: 2/21/2019 4:39:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PersonalIncomeTax_Save]
@AutoID			int,
@TaxNo			int,
@StartDate		datetime,
@EndDate		datetime,
@Status			int,
@FromAmount		float,
@ToAmount		float,
@CurrencyID		int,
@ProgressiveTax	float,
@RateTax		float,
@CountryID		int,
@Note			nvarchar(max)
AS
BEGIN
DECLARE @PersonalIncomeTax_Save int
DECLARE @MaxTaxNo int	
	select @PersonalIncomeTax_Save = count(*) from PersonalIncomeTax where AutoID = @AutoID	
	if (@PersonalIncomeTax_Save = 0)		
		begin
			set @MaxTaxNo = (SELECT TOP 1 TaxNo FROM  PersonalIncomeTax ORDER BY TaxNo DESC) +1
			insert into PersonalIncomeTax(TaxNo,StartDate,EndDate,Status,FromAmount,ToAmount,CurrencyID,ProgressiveTax,RateTax,CountryID,Note) values(@MaxTaxNo,@StartDate,@EndDate,@Status,@FromAmount,@ToAmount,@CurrencyID,@ProgressiveTax,@RateTax,@CountryID,@Note)
		end	
	else
		begin
			
			UPDATE  PersonalIncomeTax
			   SET 
				  StartDate = @StartDate
				 ,EndDate = @EndDate					
				 ,Status = @Status
				 ,FromAmount = @FromAmount
				 ,ToAmount=@ToAmount
				 ,CurrencyID=@CurrencyID
				 ,ProgressiveTax=@ProgressiveTax
				 ,RateTax=@RateTax
				 ,CountryID=@CountryID
				 ,Note=@Note		
				 WHERE AutoID = @AutoID
		end
END
