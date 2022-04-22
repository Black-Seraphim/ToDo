; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!
#define public Dependency_NoExampleSetup
#include "CodeDependencies.iss"

#define MyAppName "ToDo"
#define MyAppVersion "1.0.1.0"
#define MyAppPublisher "BlackSeraphim"
#define MyAppURL "https://www.hetfeld.name/"
#define MyAppExeName "ToDo.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{90D88486-C162-4ECE-AB50-33D54C31DBBD}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\BlackSeraphim\ToDo
DefaultGroupName=BlackSeraphim\ToDo
AllowNoIcons=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputDir=D:\Projekte\ToDo\Setup\ToDoSetup
OutputBaseFilename=ToDo_setup_{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64

[Code]
function InitializeSetup: Boolean;
begin
  Dependency_AddDotNet50Desktop;
  Result := True;
end;

[Languages]
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Projekte\ToDo\bin\Release\net5.0-windows\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Black.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Black"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-BlackItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Black Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Bold.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Bold"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-BoldItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Bold Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-ExtraBold.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Extra Bold"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-ExtraBoldItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Extra Bold Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-ExtraLight.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Extra Light"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-ExtraLightItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Extra Light Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Italic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Light.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Light"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-LightItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Light Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Medium.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Medium"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-MediumItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Medium Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Regular.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Regular"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-SemiBold.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Semi Bold"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-SemiBoldItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Semi Bold Italic"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-Thin.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Thin"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "D:\Projekte\ToDo\Fonts\Poppins\Poppins-ThinItalic.ttf"; DestDir: "{autofonts}"; FontInstall: "Poppins Thin Italic"; Flags: onlyifdoesntexist uninsneveruninstall

; download netcorecheck_x64.exe: https://go.microsoft.com/fwlink/?linkid=2135504
Source: "netcorecheck_x64.exe"; Flags: dontcopy noencryption

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

