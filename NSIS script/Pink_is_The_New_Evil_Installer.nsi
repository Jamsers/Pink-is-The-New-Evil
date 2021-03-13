# define name of installer
OutFile "PinkIsTheNewEvil_Installer.exe"
 
# define installation directory
InstallDir "$PROGRAMFILES\Pink is The New Evil"
 
# For removing Start Menu shortcut in Windows 7
RequestExecutionLevel admin
 
# start default section
Section
 
    # set the installation directory as the destination for the following actions
    SetOutPath "$INSTDIR\Pink_is_The_New_Evil_Data"
	
	File /r "..\Build\Pink_is_The_New_Evil_Data\"
 
	SetOutPath $INSTDIR
 
	# specify file to go in output path
	File "..\Build\Pink_is_The_New_Evil.exe"
 
    # create the uninstaller
    WriteUninstaller "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
	
	# write registry values for uninstaller
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil" \
                 "DisplayName" "Pink is The New Evil"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil" \
                 "UninstallString" "$\"$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe$\""
 
    # create a shortcut named "new shortcut" in the start menu programs directory
    # point the new shortcut at the program uninstaller
	CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Pink is The New Evil.lnk" "$INSTDIR\Pink_is_The_New_Evil.exe"
    CreateShortcut "$SMPROGRAMS\Pink is The New Evil\Uninstall Pink is The New Evil.lnk" "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
SectionEnd
 
# uninstaller section start
Section "uninstall"
	# now delete installed file
	Delete "$INSTDIR\Pink_is_The_New_Evil.exe"
	RMDir /r "$INSTDIR\Pink_is_The_New_Evil_Data"
 
    # second, remove the link from the start menu
	RMDir /r  "$SMPROGRAMS\Pink is The New Evil"
	
	# delete reg keys for uninstaller
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\PinkIsTheNewEvil"
	DeleteRegKey HKCU "Software\Jamsers\PinkIsTheNewEvil"
	
	# first, delete the uninstaller
    Delete "$INSTDIR\PinkIsTheNewEvil_Uninstaller.exe"
 
    RMDir $INSTDIR
# uninstaller section end
SectionEnd