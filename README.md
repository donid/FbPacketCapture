# FbPacketCapture

![build workflow](https://github.com/donid/FbPacketCapture/actions/workflows/dotnet.yml/badge.svg)

**Deutscher Text befindet sich weiter unten**

Simplify sending FritzBox PCAP packet-captures to Wireshark to allow easy monitoring of internet traffic

FbPacketCapture is a utility to simplify the process of aquiring real-time network dumps
from an AVM FritzBox and sending them to Wireshark.

**FbPacketCapture is not in any way related to AVM!**

FbPacketCapture instructs your FritzBox to mirror all traffic from and to the Internet to a locally installed Wireshark.
You can then use the "Statistics/Conversations" Menu to monitor which host on your local network is "talking" to which host
on the internet (and how many packets/bytes are transferred). Don't forget to enable these checkboxes in Wireshark's main menu:
- View/Name Resolution/Resolve Network Addresses
- View/Name Resolution/Resolve Transport Addresses
and of course "Name resolution" on the bottom of the Conversations-Window.

You must create & switch your Fritz!Box to usernamed-based login authentification!

The provided credentials of a FritzBox user account need permission to change FritzBox settings.
(Enabling 'Change Settings' will enable 'Voice and Fax Messages' and 'Smarthome', too.)
If they don't have these permissions, Wireshark will show a Dialog with this error:
"Data written to the pipe is neither in a supported pcap format nor in pcapng format."

If you want to delete your saved credentials use 'cmdkey /delete:FbPacketCapture_User_FritzBox' in a console window.
('cmdkey /list' shows all saved credentials)
Alternatively you can use 'control keymgr.dll' on a command-line to show Windows Credential Manager and choose 'Windows Credentials'.

##Deutscher Text
FbPacketCapture ist ein Hilfsprogramm, um PCAP Packetaufzeichnungen von einer AVM FritzBox automatisch in Echtzeit an Wireshark weiterzuleiten.

**FbPacketCapture ist in keiner Form mit AVM assoziiert**

FbPacketCapture steuert die FritzBox um den gesamten Datenverkehr in und aus dem Internet zum Heimnetz an ein (auf dem Rechner installiertes) Wireshark zu spiegeln. Durch dieses Netwerk-Monitoring kann man herausfinden, welcher lokale Host mit welchem Internet-Host kommuniziert und welches Gerät viel Internet-Traffic erzeugt. Wireshark hat dafür das Menü "Statistiken/Verbindungen". Damit die Hostnamen der Rechner/Geräte angezeigt werden, müssen vorher diese Optionen aktiviert werden:
- Ansicht/Namensauflösung/Netzwerkadressen auflösen
- Ansicht/Namensauflösung/Transportadressen auflösen
und natürlich "Namensauflösung" unten im "Conversations" [sic!] Fenster.

Die FritzBox muss auf User/Passwort Authentifizierung gestellt sein! Der anzugebende Benutzer muss über die Berechtigung "Einstellungen ändern" verfügen. (Wenn man diese einschaltet, werden automatisch auch "Sprach- und Faxnachrichten und "Smarthome" aktiviert). Wenn diese Berechtigungen fehlen, zeigt Wireshark eine Fehlermeldung:
"Data written to the pipe is neither in a supported pcap format nor in pcapng format."

Um die gespeicherten Zugangsdaten zu löschen kann dieser Befehl auf der Kommandozeile verwendet werden:
'cmdkey /delete:FbPacketCapture_User_FritzBox'
('cmdkey /list' zeigt alle gespeicherten Zugangsdaten)
Alternativ startet der Befehl 'control keymgr.dll' den Windows Credential Manager - darin die Seite 'Windows Credentials' aktivieren.
