if exist temp RD /S /Q temp
mkdir temp
forfiles /P ..\Tables\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\1.Setup_Tables.sql"
forfiles /P ..\ClearData\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\2.ClearData.sql"
forfiles /P ..\Types\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\3.Setup_UserDefinedTypes.sql && echo. >> ..\Setup\temp\3.Setup_UserDefinedTypes.sql"
rem forfiles /P ..\Views\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\4.Setup_Views.sql && echo. >> ..\Setup\4.Setup_Views.sql"

cmd /c type  headerProcedures-donotdelete.txt >> ..\Setup\temp\5.Setup_Functions.sql
forfiles /P ..\Functions\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\5.Setup_Functions.sql && echo. >> ..\Setup\temp\5.Setup_Functions.sql"
cmd /c type  footerProcedures-donotdelete.txt >> ..\Setup\temp\5.Setup_Functions.sql

cmd /c type  headerProcedures-donotdelete.txt >> ..\Setup\temp\6.Setup_Procedures.sql
forfiles /P ..\Procedures\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\6.Setup_Procedures.sql && echo. >> ..\Setup\temp\6.Setup_Procedures.sql && echo GO >> ..\Setup\temp\6.Setup_Procedures.sql"
cmd /c type  footerProcedures-donotdelete.txt >> ..\Setup\temp\6.Setup_Procedures.sql
rem forfiles /P ..\Data\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\7.Setup_Data.sql && echo. >> ..\Setup\temp\7.Setup_Data.sql"
