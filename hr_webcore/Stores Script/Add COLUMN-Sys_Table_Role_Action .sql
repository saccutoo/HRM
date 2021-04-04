
USE ERP_HRM
ALTER TABLE dbo.Sys_Table_Role_Action ADD isIndex BIT NULL, isGet BIT NULL
GO
UPDATE dbo.Sys_Table_Role_Action SET isIndex=1,isGet=1
GO
ALTER TABLE Sys_Table_Role_Action ALTER COLUMN isIndex BIT NOT NULL
ALTER TABLE Sys_Table_Role_Action ALTER COLUMN isGet BIT NOT NULL