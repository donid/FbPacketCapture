# FbPacketCapture
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

![build workflow](https://github.com/donid/FbPacketCapture/actions/workflows/dotnet.yml/badge.svg)
