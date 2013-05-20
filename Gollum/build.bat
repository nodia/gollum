setlocal

set DefaultFrameworkDir=%WINDIR%\Microsoft.NET\Framework

if not defined FrameworkDir (
  set FrameworkDir=%DefaultFrameworkDir%
)

if not exist %FrameworkDir%\v4.0.30319 (
  echo.
  echo .NET Framework 4.0 required.
  echo.
  goto error
)

set msbuild=%FrameworkDir%\v4.0.30319\MSBuild.exe

%msbuild% Gollum.sln /t:Build /p:Configuration=Debug
%msbuild% Gollum.sln /t:Build /p:Configuration=Release

endlocal
