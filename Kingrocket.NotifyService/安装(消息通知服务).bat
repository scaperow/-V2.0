echo �밴�������ʼ��װ��Ϣ֪ͨ����TestDataUploadWS���ĺ�̨����. . .
echo.
pause
echo.
C:\Windows\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe %~dp0Kingrocket.NotifyService.exe >> InstallService.log
echo ����װ��ϣ���������. . .
net start TestDataUploadWS >> InstallService.log
echo.
echo �������������� InstallService.log �в鿴����Ĳ��������
echo.
pause