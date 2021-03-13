Unicode True

!include "MUI2.nsh"
!define MUI_ICON "PinkIsTheNewEvil_Icon.ico"

# Define name of installer
OutFile "PinkIsTheNewEvil_Installer.exe"
 
# Define install directory
InstallDir "$PROGRAMFILES64\Pink is The New Evil"
 
# Define execution priviledge level
RequestExecutionLevel admin
 
# Installer code
Section
    # Set the destination path for the following operations
    SetOutPath "$INSTDIR\Pink_is_The_New_Evil_Data"
	
	# Install folder in dev env contents into destination path
	File /r "..\Build\Pink_is_The_New_Evil_Data\"
 
	# Set the destination path for the following operations
	SetOutPath $INSTDIR
 
	# Install file in dev env into destination path
	File "..\Build\Pink_is_The_New_Evil.exe"
 
    # Create the uninstaller
    WriteUninstaller "PinkIsTheNewEvil_Uninstaller.exe"
	
	# Create directory for shortcuts in start menu
	CreateDirectory "$SMPROGRAMS\Pink is The New Evil"
	
	# Create shortcuts in the directory, and point them at the executables
	CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Pink is The New Evil.lnk" "$INSTDIR\Pink_is_The_New_Evil.exe"
    CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Uninstall Pink is The New Evil.lnk" "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
	
	# Write registry values for uninstaller
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil" \
                 "DisplayName" "Pink is The New Evil"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil" \
                 "UninstallString" "$\"$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe$\""
SectionEnd
 
# Uninstaller code
Section "uninstall"
	# Delete installed files and folders
	Delete "$INSTDIR\Pink_is_The_New_Evil.exe"
	RMDir /r "$INSTDIR\Pink_is_The_New_Evil_Data"
 
    # Delete start menu shortcuts folder
	RMDir /r  "$SMPROGRAMS\Pink is The New Evil"
	
	# Delete reg keys for uninstaller
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil"
	
	# Delete installer
    Delete "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
 
	# Delete install directory
    RMDir $INSTDIR
SectionEnd