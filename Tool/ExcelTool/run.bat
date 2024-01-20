@echo "开始转表"

@set unityPath=..\..\RPGCode
@set codePath=Assets\Scripts\Game\Configs\Types
@set dataPath=Assets\Resources\Config\Table
@set TempunityPath=Out
@set TempcodePath=Scripts
@set TempdataPath=Config


@echo 正在清理数据
@if exist "Program\out\code" (
@rd "Program\out\code" /s /q
)

@if exist "Program\out\data" (
@rd "Program\out\data" /s /q
)

@echo 初始化静态数据
@if not exist "Program\out\code" (
@md "Program\out\code"
)

@if not exist "Program\out\tmp" (
@md "Program\out\tmp"
)

@if not exist "Program\out\code\ResType" (
@md "Program\out\code\ResType"
)
@if not exist "Program\out\code\ResEnum" (
@md "Program\out\code\ResEnum"
)
@if not exist "Program\out\code\UserType" (
@md "Program\out\code\UserType"
)
@if not exist "Program\out\code\ResValue" (
@md "Program\out\code\ResValue"
)
@if not exist "Program\out\data" (
@md "Program\out\data"
)

@echo 开始转换文件

@For /r ExcelConfig %%i In (*.*) Do @echo 正在转换  %%~ni & @python Program\ExcelLoader.py %%i -all

rd "%TempunityPath%\%TempcodePath%" /s /q
rd "%TempunityPath%\%TempdataPath%" /s /q
rd "%unityPath%\%codePath%" /s /q
rd "%unityPath%\%dataPath%" /s /q

xcopy "Program\out\code" "%TempunityPath%\%TempcodePath%\" /D /S /E /Y
xcopy "Program\out\data" "%TempunityPath%\%TempdataPath%\" /D /S /E /Y
xcopy "Program\out\code" "%unityPath%\%codePath%\" /D /S /E /Y
xcopy "Program\out\data" "%unityPath%\%dataPath%\" /D /S /E /Y

@rd "Program\out/code" /s /q
@rd "Program\out/data" /s /q
@rd "Program\out/tmp" /s /q

pause