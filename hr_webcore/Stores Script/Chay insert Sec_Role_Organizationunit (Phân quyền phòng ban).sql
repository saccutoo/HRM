INSERT INTO [dbo].[Sec_Role_Organizationunit]
           ([OrganizationunitID]
           ,[RoleID])
		   select Organizationunitid,3 from Organizationunit where name like '%BD%' 

   

INSERT INTO [dbo].[Sec_Role_Organizationunit]
           ([OrganizationunitID]
           ,[RoleID])
		   select Organizationunitid,4 from Organizationunit where name like '%media%' 

		   

INSERT INTO [dbo].[Sec_Role_Organizationunit]
           ([OrganizationunitID]
           ,[RoleID])
		   select Organizationunitid,5 from Organizationunit where name like '%Cs%'  or name like '%account%'