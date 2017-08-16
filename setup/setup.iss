[Files]
Source: "ipfs-386\go-ipfs\ipfs.exe"; DestDir: "{app}\bin"; DestName: "ipfs.exe"; Flags: 32bit
Source: "ipfs-amd64\go-ipfs\ipfs.exe"; DestDir: "{app}\bin"; DestName: "ipfs.exe"; Flags: 64bit
Source: "..\bin\Release\ipfs-gui.exe"; DestDir: "{app}\bin"; DestName: "ipfs-gui.exe"
Source: "ipfs.ico"; DestDir: "{app}"; DestName: "ipfs.ico"

[Tasks]
Name: "DesktopIcon"; Description: "Create a desktop icon"
Name: "SendTo"; Description: "Add ipfs to ""Send to"""
Name: "Autostart"; Description: "Autostart ipfs-gui"
Name: "AddToPath"; Description: "Add IPFS to path"

[Icons]
Name: "{sendto}\Add to IPFS"; Filename: "{app}\bin\ipfs-gui.exe"; WorkingDir: "{app}\bin"; IconFilename: "{app}\ipfs.ico"; IconIndex: 0; Tasks: SendTo
Name: "{userdesktop}\IPFS-GUI"; Filename: "{app}\bin\ipfs-gui.exe"; WorkingDir: "{app}\bin"; IconFilename: "{app}\ipfs.ico"; IconIndex: 0; Tasks: DesktopIcon
Name: "{userstartup}\IPFS-GUI"; Filename: "{app}\bin\ipfs-gui.exe"; WorkingDir: "{app}\bin"; IconFilename: "{app}\ipfs.ico"; IconIndex: 0; Tasks: Autostart

[Setup]
AppName=IPFS-GUI
AppVersion=0.0.1
AppCopyright=marcin212
AppId={{C8DAD47B-4EF3-4570-98B2-3D066110C530}
LicenseFile=License.txt
SetupIconFile=.\ipfs.ico
AppPublisher=marcin212
AppPublisherURL=https://github.com/marcin212/ipfs-gui
AppSupportURL=https://github.com/marcin212/ipfs-gui
AppUpdatesURL=https://github.com/marcin212/ipfs-gui
AppComments=https://github.com/marcin212/ipfs-gui
AppContact=https://github.com/marcin212/ipfs-gui
AppReadmeFile=https://github.com/marcin212/ipfs-gui
VersionInfoVersion=0.0.1
VersionInfoCompany=Starchasers
VersionInfoDescription=GUI for IPFS
VersionInfoTextVersion=0.0.1
VersionInfoCopyright=marcin212
VersionInfoProductName=0.0.1
VersionInfoProductVersion=0.0.1
VersionInfoProductTextVersion=0.0.1
DefaultDirName={pf}\ipfs-gui
UninstallDisplayName=IPFS-GUI
UninstallDisplaySize=70
UninstallDisplayIcon={app}\ipfs.ico
OutputBaseFilename=IPFS-GUI-Setup

[Registry]
Root: "HKLM"; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}\bin"; Tasks: AddToPath; Check: NeedsAddPath('{app}\bin')
Root: "HKCU"; Subkey: "Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}\bin"; Tasks: AddToPath; Check: NeedsAddPathUser('{app}\bin')

[Run]
Filename: "{app}\bin\ipfs-gui.exe"; WorkingDir: "{app}\bin"; Flags: postinstall nowait; Description: "Run IPFS-GUI now"

[Dirs]
Name: "{app}\bin"

[Code]
// https://stackoverflow.com/a/3431379/7462702
function NeedsAddPath(Param: string): boolean;
var
  OrigPath: string;
begin
  if not RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
    'Path', OrigPath)
  then begin
    Result := True;
    exit;
  end;
  { look for the path with leading and trailing semicolon }
  { Pos() returns 0 if not found }
  Result := Pos(';' + Param + ';', ';' + OrigPath + ';') = 0;
end;


function NeedsAddPathUser(Param: string): boolean;
var
  OrigPath: string;
begin
  if not RegQueryStringValue(HKEY_CURRENT_USER,
    'Environment',
    'Path', OrigPath)
  then begin
    Result := True;
    exit;
  end;
  { look for the path with leading and trailing semicolon }
  { Pos() returns 0 if not found }
  Result := Pos(';' + Param + ';', ';' + OrigPath + ';') = 0;
end;