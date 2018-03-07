echo 请按任意键开始安装消息通知服务（TestDataUploadWS）的后台服务. . .
echo.
pause
echo.
C:\Windows\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe %~dp0Kingrocket.NotifyService.exe >> InstallService.log
echo 服务安装完毕，启动服务. . .
net start TestDataUploadWS >> InstallService.log
echo.
echo 操作结束，请在 InstallService.log 中查看具体的操作结果。
echo.
pause