# Pink-is-The-New-Evil
A top down whack-a-mole action game. Available for Windows, on itch.io.  
Originally published as an Android game.

![Battleships-Clash](https://github.com/user-attachments/assets/9b31433a-e72b-439d-b57f-1674e0e3e598)

## Releases

"Pink is The New Evil!" can be downloaded from itch.io!

[!["Pink is The New Evil!" on itch.io](https://img.shields.io/badge/Download_on-itch.io-red?style=for-the-badge&logo=itch.io&logoColor=white)](https://jamsers.itch.io/pink-is-the-new-evil)

## Development Environment

Built in Unity 5.6.7. Written in C#. Windows installer built with NSIS. 3D models edited with Blender 2.79. Images edited with GIMP. Textures made with Substance Painter. Audio edited with Audacity.  
Music made with FL Studio.

[![Unity 5.6.7](https://img.shields.io/badge/Unity-5.6.7-222C37?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/releases/editor/whats-new/5.6.7) 
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://dotnet.microsoft.com/languages/csharp) 
[![NSIS](https://img.shields.io/badge/NSIS-blue.svg?style=for-the-badge&logo=nsis&logoColor=white)](https://nsis.sourceforge.io/Main_Page) 
[![Blender 2.79](https://img.shields.io/badge/Blender-2.79-F5792A?style=for-the-badge&logo=blender&logoColor=white)](https://download.blender.org/release/Blender2.79/) 
[![GIMP](https://img.shields.io/badge/GIMP-5C5C5C?style=for-the-badge&logo=gimp&logoColor=white)](https://www.gimp.org/) 
[![Substance Painter](https://img.shields.io/badge/Substance_Painter-FF3C2A?style=for-the-badge&logo=adobe&logoColor=white)](https://www.adobe.com/products/substance3d/apps/painter.html) 
[![Audacity](https://img.shields.io/badge/Audacity-0000CC?style=for-the-badge&logo=audacity&logoColor=white)](https://www.audacityteam.org/) 
[![FL Studio](https://img.shields.io/badge/FL_Studio-EC4E36?style=for-the-badge&logo=fl-studio&logoColor=white)](https://www.image-line.com/) 

## How to Build
Pink-is-The-New-Evil is built in Windows.

1. Install [Unity 5.6.7](https://unity.com/releases/editor/whats-new/5.6.7). No other modules required.
1. Install [Blender 2.79](https://download.blender.org/release/Blender2.79/). Blender 2.79 is the latest version of Blender that still works with Unity 5's `.blend` importer.
1. Clone or download this repository. Open the repository folder with Unity.
1. Go to File and select Build & Run.

To build the Windows installer:

1. Install [NSIS](https://nsis.sourceforge.io/Main_Page).
1. Open the repository folder, go to `.\NSIS script\`, and compile `Pink_is_The_New_Evil_Installer.nsi` with the NSIS compiler. (`makensisw.exe`)

> [!IMPORTANT]  
> The NSIS script expects you to have built at `Build\Pink_is_The_New_Evil.exe`.

## License

Unless stated otherwise within the [**`ATTRIBUTION`**](ATTRIBUTION) file or directly alongside specific files/folders, the following licenses apply:

**Code:** Licensed under the MIT license.  
[![MIT license](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE-CODE)

**Assets:** Licensed under the CC BY 4.0 license.  
[![CC BY 4.0 license](https://img.shields.io/badge/License-CC_BY_4.0-lightgrey.svg?style=for-the-badge)](LICENSE-ASSETS)

Please refer to the respective license files for full details.

## Credits

Developed by [**John James Gutib**](https://github.com/Jamsers).

Music by [**Aaron James Gutib**](https://www.youtube.com/@Anuron01/).

Please refer to the [**`ATTRIBUTION`**](ATTRIBUTION) file for full details.
