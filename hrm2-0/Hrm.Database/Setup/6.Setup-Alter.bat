@echo off
SETLOCAL
call 1.PreSetup.bat
echo server: %SERVER%
echo username: %USERNAME%
chcp 65001
:createScript
echo "Preparing database scripts..."
if exist temp RD /S /Q temp
mkdir temp
cmd /c type  headerProcedures-donotdelete.txt >> ..\Setup\temp\7.Setup_Alter.sql
forfiles /P ..\Alters\ /m *.sql /c "cmd /c type @path >> ..\Setup\temp\7.Setup_Alter.sql && echo. >> ..\Setup\temp\7.Setup_Alter.sql"
cmd /c type  footerProcedures-donotdelete.txt >> ..\Setup\temp\7.Setup_Alter.sql
echo "Preparation completed."

echo "Checking and creating tables..."
sqlcmd -U %USERNAME% -P %PASSWORD% -S %SERVER% -d %DATABASE% -f 65001 -i temp\7.Setup_Alter.sql
if errorlevel 1 goto ERROR
echo "All script altered successfully."
echo "Done."
goto DONE

:ERROR
echo "Database is deployed unsuccessfully. Use uninstall command to rollback deployment."
:DONE
echo "Cleaning up..."

echo "Done."
goto exitScript
echo "You have exited altering"
:exitScript
@echo off
pause
ENDLOCAL