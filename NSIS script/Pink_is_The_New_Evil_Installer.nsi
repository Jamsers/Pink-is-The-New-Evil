Unicode True
!include "MUI2.nsh"
!include "FileFunc.nsh"
!define PUBLISHER_NAME "John James Gutib"
!define PRODUCT_NAME "Pink is The New Evil"
!define PRODUCT_VERSION "0.96.0.0"
!define OutputFileName "PinkIsTheNewEvil_Installer.exe"
!define ARP "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil"
!define MUI_ICON "PinkIsTheNewEvil_Icon.ico"
!define MUI_ABORTWARNING

!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

Name "${PRODUCT_NAME}"
OutFile "${OutputFileName}"
InstallDir "$PROGRAMFILES64\${PRODUCT_NAME}"
RequestExecutionLevel admin

!define /file OutFileSignPassword ".\CodeSign\passwd.txt"
!define OutFileSignCertificate ".\CodeSign\certificate.pfx"
!define OutFileSignSHA1   ".\CodeSign\signtool.exe sign /f ${OutFileSignCertificate} /p ${OutFileSignPassword} /fd sha1   /t  http://timestamp.comodoca.com /v" 
!define OutFileSignSHA256 ".\CodeSign\signtool.exe sign /f ${OutFileSignCertificate} /p ${OutFileSignPassword} /fd sha256 /tr http://timestamp.comodoca.com?td=sha256 /td sha256 /as /v" 

/*
# Sign installer
!finalize "PING -n 1 127.0.0.1 >nul"
!finalize "${OutFileSignSHA1} .\${OutputFileName}"
!finalize "PING -n 5 127.0.0.1 >nul"
!finalize "${OutFileSignSHA256} .\${OutputFileName}"
*/

Section "install"
	# Install files
    SetOutPath "$INSTDIR\Pink_is_The_New_Evil_Data"
	File /r "..\Build\Pink_is_The_New_Evil_Data\"
	SetOutPath $INSTDIR
	File "..\Build\Pink_is_The_New_Evil.exe"
	File "PinkIsTheNewEvil_Icon.ico"
 
    # Create the uninstaller
    WriteUninstaller "PinkIsTheNewEvil_Uninstaller.exe"
	
	# Calculate total size of files installed
	${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
	IntFmt $0 "0x%08X" $0
	
	# Write reg keys for uninstaller
	WriteRegStr HKLM "${ARP}" \
                 "DisplayName" "${PRODUCT_NAME}"
	WriteRegStr HKLM "${ARP}" \
                 "UninstallString" "$\"$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe$\""
	WriteRegStr HKLM "${ARP}" \
                 "InstallLocation" "$\"$INSTDIR$\""
	WriteRegStr HKLM "${ARP}" \
                 "Publisher" "${PUBLISHER_NAME}"
	WriteRegStr HKLM "${ARP}" \
                 "DisplayVersion" "${PRODUCT_VERSION}"
	WriteRegStr HKLM "${ARP}" \
                 "DisplayIcon" "$\"$INSTDIR\PinkIsTheNewEvil_Icon.ico$\""
	WriteRegDWORD HKLM "${ARP}" "EstimatedSize" "$0"
	
	# Create shortcuts
	CreateDirectory "$SMPROGRAMS\Pink is The New Evil"
	CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Pink is The New Evil.lnk" "$INSTDIR\Pink_is_The_New_Evil.exe"
    CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Uninstall Pink is The New Evil.lnk" "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
SectionEnd

Section "uninstall"
	# Delete files
	RMDir /r "$INSTDIR\Pink_is_The_New_Evil_Data"
	Delete "$INSTDIR\Pink_is_The_New_Evil.exe"
	Delete "$INSTDIR\PinkIsTheNewEvil_Icon.ico"
	
	# Delete reg keys for uninstaller
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil"
 
    # Delete shortcuts
	RMDir /r  "$SMPROGRAMS\Pink is The New Evil"
	
	# Delete uninstaller and install dir
    Delete "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
    RMDir $INSTDIR
SectionEnd