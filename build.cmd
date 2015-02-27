@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

msbuild TAlex.MathCore.sln /p:Configuration="%config%" /p:BuildPackage=true