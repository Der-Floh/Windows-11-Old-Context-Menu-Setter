# Windows 11 Old Context Menu-Setter
[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/Der-Floh/Windows-11-Old-Context-Menu-Setter/blob/master/README.md)

Ein Programm, welches das alte "Rechtsclickmenü" in Windows 11 aktiviert. So muss man nicht immer "Mehr anzeigen" clicken. Es funktioniert, indem es einige Registry keys setzt (Wie viele online Tutorials auch zeigen)

Aktuelle Version kann unter [Releases](https://github.com/Der-Floh/Windows-11-Old-Context-Menu-Setter/releases) gefunden werden

## Nutzung
Nach dem ausführen des Programms muss nur der explorer neugestartet werden, um die Änderungen zu bestätigen.

## Wie es zu entfernen ist
Wenn der key schon existiert wird das Programm fragen, ob man es entfernen möchte.

#### Aber man kann es auch manuell machen:
Um es wieder zu entfernen drück **windows + r** und schreib **regedit** dann navigiere zum Pfad:
**Computer\HKEY_CURRENT_USER\Software\Classes\CLSID** und lösche den key: **{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}**
Der explorer muss neugestartet werden um die Änderungen abzuschließen.

## Kann ich das auch manuell machen?
Ja. Indem man diesem Tutorial folgt: https://pureinfotech.com/bring-back-classic-context-menu-windows-11

Oder indem man folgenden cmd Befehl, aus einem Microsoft Post, benutzt: https://answers.microsoft.com/en-us/windows/forum/all/restore-legacy-right-click-menu-for-file-explorer/a62e797c-eaf3-411b-aeec-e460e6e5a82a
