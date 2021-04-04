USE [ERP_v2]
GO

 
alter FUNCTION [dbo].[F_ReportAccount_GET]
	(
		@LanguageID					INT			 = NULL,
		@DateFrom					CHAR(19)	 = NULL,
		@DateTo						CHAR(19)	 = NULL,
		@CurrencyID					CHAR(5)		 = 0, 
		@LastDOM					CHAR(19)	 = NULL
	)
RETURNS TABLE
as 
RETURN 
		SELECT 
					b.MCCAccountID,b.StaffID
					,b.AccountNumber,StatusAccount=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.StatusId = g.GlobalListID)
					,AccountLevel=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountLevelId = g.GlobalListID),IsPartner,FirstDateLinked,BD,Department=o.Name,
					AccountType= (select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountTypeId = g.GlobalListID)
					,T,AccountConversion ,FeeAmount, Margin,b.StatusId as RankId,b.OrganizationUnitID,b.AccountTypeId,b.AccountLevelId,b.CustomerId,CustomerCode=(select CustomerCode from ERP_v2.dbo.customer c where c.customerid=b.customerid)
					from ERP_v2.dbo.HistoryPay b  
					left join Organizationunit o on o.OrganizationUnitID=b.OrganizationUnitID
					where PayDate>=@DateFrom and PayDate<=@DateTo

					-- lấy những tài khoản unlink, reulink chưa qua 30 ngày để trừ
			union all
				SELECT 
					b.MCCAccountID,b.StaffID
					,b.AccountNumber,StatusAccount=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.RankId = g.GlobalListID)
					,AccountLevel=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountLevelId = g.GlobalListID)
					,b.IsPartner,b.FirstDateLink,b.BD
					,Department=(select iif(@languageID=4,o.NameEN,o.Name) from dbo.Organizationunit o where b.OrganizationUnitID = o.OrganizationUnitID),
					AccountType= (select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountTypeId = g.GlobalListID)
					,T=(-1)*sum(T) 
					,AccountConversion=(-1)*sum(AccountConversion)
					,0 FeeAmount,0 Margin,b.RankId  ,b.OrganizationUnitID,b.AccountTypeId,b.AccountLevelId,b.CustomerId,CustomerCode=(select CustomerCode from ERP_v2.dbo.customer c where c.customerid=b.customerid)
				 
				from ERP_v2.dbo.F_MCCAccount_Gets(@languageID,CONVERT(CHAR(10),@DateTo,121),CONVERT(CHAR(10),@DateTo,121)) b
				INNER JOIN ERP_v2.dbo.HistoryPay h ON h.MCCAccountID=b.MCCAccountID 
				
				WHERE  b.lastDateUnlink BETWEEN @DateFrom AND @DateTo AND b.passtrialtime=0 AND (b.status=1705 or b.status=1801)
				GROUP BY b.MCCAccountID,b.StaffID,b.OrganizationUnitID,b.AccountTypeId,b.RankId ,b.AccountNumber,b.AccountLevelId,b.IsPartner,b.FirstDateLink,b.BD, b.CustomerID

	
			UNION ALL
				-- lấy những tài khoản link, relink để cộng
				
				SELECT	
					b.MCCAccountID,b.StaffID
					,b.AccountNumber,StatusAccount=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.RankId = g.GlobalListID)
					,AccountLevel=(select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountLevelId = g.GlobalListID)
					,b.IsPartner,IIF(b.LastDateLink<@DateFrom ,@LastDOM,b.FirstDateLink) FirstDateLink,b.BD
					,Department=(select iif(@languageID=4,o.NameEN,o.Name) from dbo.Organizationunit o where b.OrganizationUnitID = o.OrganizationUnitID)
					,AccountType= (select iif(@languageID=4,g.NameEN,g.Name) from GlobalList g where b.AccountTypeId = g.GlobalListID)
					, T=
						IIF(b.CheckPartner>0,0.5,1)*
						IIF(Datinh>0 and bod.StaffID is null,0,1) 

						*iif( ((CountInvalid>1 or CountSub>1) and DATEDIFF(day,FirstDateLink,getdate())>90 ) and  bod.StaffID is  null,0,1)
						* iif(Datinh>0,0,1) --??????????
					, AccountConversion=
						IIF(b.CheckPartner>0,0.5,1)*
						IIF(Datinh>0 and bod.StaffID is null,0,1) 
						
						*iif( ((CountInvalid>1 or CountSub>1) and DATEDIFF(day,FirstDateLink,getdate())>90 ) and  bod.StaffID is  null,0,1)
						* (iif(RankID=1729,1,iif(RankID=3399,5,iif(RankID=3398,10,0)))
						- ISNULL(IIF(countBODHis=0,0,HisAccountConversion),0))
					,0 FeeAmount,0 Margin,b.RankId ,b.OrganizationUnitID,b.AccountTypeId,b.AccountLevelId,b.CustomerId,CustomerCode=(select CustomerCode from ERP_v2.dbo.customer c where c.customerid=b.customerid)
				from ERP_v2.dbo.F_MCCAccount_Gets(@languageID,CONVERT(CHAR(10),@DateTo,121),CONVERT(CHAR(10),@DateTo,121)) b
				left join ERP_v2.dbo.BODAllowForSale bod on bod.AccountNumber=b.AccountNumber and b.staffid=bod.StaffID
				LEFT JOIN (	
						SELECT AccountNumber,T=sum(T),HisAccountConversion=Sum(AccountConversion) ,CountInvalid=sum(iif([RankID]= 1727,1,0)) 
						,CountSub=sum(iif([RankID]= 1728,1,0)),countBODHis=SUM(iif(IsBODAllowForSale=1,1,0))
						FROM ERP_v2.dbo.HistoryPay 
						GROUP BY AccountNumber ) h ON b.AccountNumber=h.AccountNumber 
				OUTER APPLY (
						SELECT Datinh= Count(MCCAccountID) 
						FROM ERP_v2.dbo.HistoryPay k 
						WHERE ([RankID] <> 1727 or  [RankID]<> 1728) and k.AccountNumber=b.AccountNumber and T!=0 ) dh
				WHERE --( b.RankId=1729 OR b.RankId=3399 OR b.RankId = 3398)  AND
						 (status=1704 or status=1747)  and 
						 ((b.LastDateLink BETWEEN @DateFrom AND @DateTo )
						 or (b.LastDateLink<=@DateFrom  and DATEDIFF(day,FirstDateLink,getdate())<=90  AND
						 Datinh<=0 AND b.RankId <> 1727 AND b.RankId <> 1728))
						 GROUP BY b.MCCAccountID,b.StaffID,b.OrganizationUnitID,b.AccountTypeId,b.CheckPartner,b.RankId ,b.AccountNumber, b.LastDateLink
							,b.AccountLevelId,b.IsPartner,b.FirstDateLink,b.BD, b.CustomerID, h.CountInvalid, h.CountSub, dh.Datinh, bod.StaffId, h.HisAccountConversion, h.countBODHis
			
			-- union thêm với select * FROM erp_v2.dbo.F_ReportMarginBD('194')  where thêm payment date để lấy margin
			-- payment product
			UNION ALL 
				SELECT 
					0 AS MCCAccountID,b.StaffID
					,0 AS AccountNumber,'' AS StatusAccount
					,'' AS AccountLevel, '' AS IsPartner, b.PaymentDate AS FirstDateLinked,'' AS BD
					,Department=(select iif(@languageID=4,o.NameEN,o.Name) from dbo.Organizationunit o where b.OrganizationUnitID = o.OrganizationUnitID)  
					,'' AS AccountType
					,0 AS T
					,b.Amount/isnull((select value from ERP_HRM.dbo.Globallist where globallistid=3403),1) AS AccountConversion 
					,0 AS FeeAmount
					,b.Amount AS Margin,0 AS RankId,b.OrganizationUnitID,0 AS AccountTypeId,0 AS AccountLevelId
					,0 AS CustomerId, '' AS CustomerCode					
				FROM erp_v2.dbo.F_ReportMarginBD(@CurrencyID) b
				WHERE b.PaymentDate BETWEEN @DateFrom and @DateTo

			--union với PaymentProduct để lấy spending(fee amount)
			UNION ALL 
				SELECT 
					0 AS MCCAccountID,b.StaffID
					,0 AS AccountNumber,'' AS StatusAccount
					,'' AS AccountLevel, '' AS IsPartner, b.PaymentDate AS FirstDateLinked,'' AS BD
					,Department=(select iif(@languageID=4,o.NameEN,o.Name) from dbo.Organizationunit o where b.OrganizationUnitID = o.OrganizationUnitID)  
					,'' AS AccountType
					,0 AS T
					, b.Amount/isnull((select value from ERP_HRM.dbo.Globallist where globallistid=3402),1) AS AccountConversion
					,b.Amount AS FeeAmount
					,0 AS Margin,0 AS RankId,b.OrganizationUnitID,0 AS AccountTypeId,0 AS AccountLevelId
					,0 AS CustomerId, '' AS CustomerCode	
				FROM ERP_HRM.dbo.PaymentProduct b
				LEFT JOIN dbo.Organizationunit o ON o.OrganizationUnitID = b.OrganizationunitID
				WHERE b.PaymentDate BETWEEN @DateFrom and @DateTo
