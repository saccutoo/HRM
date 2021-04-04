USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Config_PersonalIncomeTax_Save]    Script Date: 1/30/2019 5:29:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Config_PersonalIncomeTax_Save]
@ID						int,
@FromIncome				float,
@Tax					float,
@ProgressiveAmount		float,
@FullAmount				float,
@SubtractAmount			float
AS
BEGIN
DECLARE @Config_PersonalIncomeTax_Save int
	select @Config_PersonalIncomeTax_Save = count(*) from Config_PersonalIncomeTax where ID = @ID
	if (@Config_PersonalIncomeTax_Save = 0)		
		begin
			insert Config_PersonalIncomeTax (FromIncome,Tax,ProgressiveAmount,FullAmount,SubtractAmount) values(@FromIncome,@Tax,@ProgressiveAmount,@FullAmount,@SubtractAmount)
		end	
	else
		begin
			UPDATE  Config_PersonalIncomeTax
			   SET 
				  FromIncome=@FromIncome
				 ,Tax = @Tax
				 ,ProgressiveAmount= @ProgressiveAmount					
				 ,FullAmount = @FullAmount
				 ,SubtractAmount = @SubtractAmount				
				 WHERE ID = @ID
		end	
END
