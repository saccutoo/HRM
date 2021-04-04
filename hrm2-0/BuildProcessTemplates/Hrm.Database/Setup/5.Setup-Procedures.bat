@echo off
SETLOCAL
call 1.PreSetup.bat
echo server: %SERVER%
echo username: %USERNAME%

chcp 65001
:createProcedures

echo "Preparing database scripts..."
call 4.CreateFiles.bat
echo "Preparation completed."

REM echo "Checking and creating tables..."
REM sqlcmd -U %USERNAME% -P %PASSWORD% -S %SERVER% -d %DATABASE% -i temp\1.Setup_Tables.sql
REM if errorlevel 1 goto ERROR
REM echo "All tables created successfully."
REM echo "Done."

echo "Clearing all procedures..."
sqlcmd -U %USERNAME% -P %PASSWORD% -S %SERVER% -d %DATABASE% -i temp\2.ClearData.sql
if errorlevel 1 goto ERROR
echo "Procedures are cleared successfully."
echo "Done."

echo "creating all user define types..."
sqlcmd -U %username% -P %password% -S %server% -d %database% -i temp\3.setup_userdefinedtypes.sql
if errorlevel 1 goto ERROR
echo "User defined types are created successfully."
echo "Done."

echo "Creating function..."
sqlcmd -U %USERNAME% -P %PASSWORD% -S %SERVER% -d %DATABASE% -f 65001 -i temp\5.Setup_Functions.sql
if errorlevel 1 goto ERROR
echo "Functions are created successfully."
echo "Done."

echo "Creating procedures..."
sqlcmd -U %USERNAME% -P %PASSWORD% -S %SERVER% -d %DATABASE% -f 65001 -i temp\6.Setup_Procedures.sql
if errorlevel 1 goto ERROR
echo "Procedures are created successfully."
echo "Done."
goto DONE

:ERROR
echo "Database is deployed unsuccessfully. Use uninstall command to rollback deployment."
:DONE
echo "Cleaning up..."
REM RD /S /Q temp
echo "Done."
goto exitProcedures

:exitProcedures

echo "You have exited create procedures"
set /p DUMMY=Hit ENTER to continue...



ENDLOCAL