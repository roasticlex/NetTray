[Setup]
AppName=MyTrayApp
AppVersion=1.0
DefaultDirName={pf}\NetTray
DefaultGroupName=NetTray
OutputDir=Output
OutputBaseFilename=NetTrayAppInstaller
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\Release\MyTrayApp.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\MyTrayApp"; Filename: "{app}\MyTrayApp.exe"
Name: "{commondesktop}\MyTrayApp"; Filename: "{app}\MyTrayApp.exe"

[Run]
Filename: "{app}\MyTrayApp.exe"; Description: "Launch MyTrayApp"; Flags: nowait postinstall skipifsilent