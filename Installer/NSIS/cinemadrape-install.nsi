; CinemaDrape Setup Program

;--------------------------------------

!include MUI.nsh ;Include Modern UI
!include WordFunc.nsh
!insertmacro VersionCompare
!include LogicLib.nsh
;--------------------------------------

;General

  RequestExecutionLevel admin

  ;The name of the installer
  Name "CinemaDrape"

  ;The setup executable file to create
  OutFile "output\cinemadrape-install.exe"

  ;The default installation directory
  InstallDir "$PROGRAMFILES64\Aurelitec\CinemaDrape"

;--------------------------------------

;Interface Settings

  !define MUI_ABORTWARNING

;--------------------------------------

;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "Files\License.txt"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES

  !define MUI_FINISHPAGE_RUN CinemaDrape.exe
  !define MUI_FINISHPAGE_RUN_TEXT "Start CinemaDrape"
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------------

;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------------

;Version Information

  VIProductVersion "2.2.0.332"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "CinemaDrape"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "Focus on your current task by blanking or dimming other screen areas."
  VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "Aurelitec"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalTrademarks" ""
  VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright © 2009-2017 Aurelitec (http://www.aurelitec.com)"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "CinemaDrape Installer"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "2.2.0.332"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductVersion" "2.2.0.332"

;--------------------------------------

Section
  ; Copy files to installation directory
  SetOutPath $INSTDIR
  File Files\CinemaDrape.exe
  File Files\CinemaDrape.exe.config
  File Files\License.txt
  File "Files\CinemaDrapeLink.html"

  ; Copy settings file to user Application Data folder
  CreateDirectory "$APPDATA\Aurelitec\CinemaDrape"
  File "/oname=$APPDATA\Aurelitec\CinemaDrape\cinemadrape.cinemadrape" Files\cinemadrape.cinemadrape

  ; Create Start Menu shortcuts
  CreateShortCut "$SMPROGRAMS\CinemaDrape.lnk" "$INSTDIR\CinemaDrape.exe" "" "" "" SW_SHOWNORMAL "" "Focus on your current task by blanking or dimming other screen areas."

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\uninstall.exe"
  SetRegView 64
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "DisplayName" "CinemaDrape"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "QuietUninstallString" "$\"$INSTDIR\uninstall.exe$\" /S" 
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "DisplayVersion" "2.2.0.332"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "DisplayIcon" "$\"$INSTDIR\cinemadrape.exe$\""
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "EstimatedSize" "181"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "HelpLink" "http://www.aurelitec.com/cinemadrape/help/"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "InstallLocation" "$INSTDIR"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "NoModify" "1"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "NoRepair" "1"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "Publisher" "Aurelitec"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "URLInfoAbout" "http://www.aurelitec.com/cinemadrape/"
  WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "URLUpdateInfo" "http://www.aurelitec.com/cinemadrape/"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "VersionMajor" "2"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape" "VersionMinor" "1"
SectionEnd

;--------------------------------------

;Uninstaller Section

Section "Uninstall"

  ; Delete files from installation directory
  Delete "$INSTDIR\uninstall.exe"
  Delete "$INSTDIR\CinemaDrape.exe"
  Delete "$INSTDIR\CinemaDrape.exe.config"
  Delete "$INSTDIR\License.txt"
  Delete "$INSTDIR\CinemaDrapeLink.html"
  RMDir "$INSTDIR"
  RMDir "$PROGRAMFILES64\Aurelitec"

  ; Delete settings file and folder from user Application Data folder
  Delete "$APPDATA\Aurelitec\CinemaDrape\cinemadrape.cinemadrape"
  RMDir "$APPDATA\Aurelitec\CinemaDrape"

  ; Delete Start Menu shortcut
  Delete "$SMPROGRAMS\CinemaDrape.lnk"

  ; Delete Uninstall registry key
  SetRegView 64
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\CinemaDrape"

SectionEnd