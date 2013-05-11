using System;
using System.Collections;
using System.Threading;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace RelayModule
{
    public partial class Program
    {
        private static bool activateRelay = false;
        void ProgramStarted()
        {
            Debug.Print("Program Started");
            relayISOx16.DisableAllRelays();
            button.ButtonPressed += button_ButtonPressed;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!activateRelay)
            {
                relayISOx16.EnableRelay(RelayISOx16.Relay.Relay_9);
                activateRelay = true;
            }
            else
            {
                relayISOx16.DisableRelay(RelayISOx16.Relay.Relay_9);
                activateRelay = false;
            }
        }
    }
}
