# Windows 11 Old Context Menu-Setter
[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/Der-Floh/Windows-11-Old-Context-Menu-Setter/blob/master/README.md)
[![de](https://img.shields.io/badge/lang-de-green.svg)](https://github.com/Der-Floh/Windows-11-Old-Context-Menu-Setter/blob/master/README.de.md)

A program that activates the old "Right Click Menu" in Windows 11. So you don't have to click "Show more" everytime. It works by setting a few Registry keys (like any online tutorial would teach you)

Current Version can be found under [Releases](https://github.com/Der-Floh/Windows-11-Old-Context-Menu-Setter/releases)

## Usage
After running the program the explorer needs to be restarted to apply the changes.

## How to Remove it
If the key already exists the program will ask if you want to delete it.

#### But you can also do it manually:
For removing this again press **windows + r** and type **regedit** then navigate to the path showed by the program:
**Computer\HKEY_CURRENT_USER\Software\Classes\CLSID** and delete the key: **{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}**
The explorer needs to be restarted to apply the changes.

## Can I do this Manually?
Yes. By following this Tutorial using the Regestry editor: https://pureinfotech.com/bring-back-classic-context-menu-windows-11

Or by using a cmd command described in this Microsoft Post: https://answers.microsoft.com/en-us/windows/forum/all/restore-legacy-right-click-menu-for-file-explorer/a62e797c-eaf3-411b-aeec-e460e6e5a82a
