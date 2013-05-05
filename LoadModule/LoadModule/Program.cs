using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace LoadModule
{
    public partial class Program
    {
        public static bool runMotor = false;
        
        void ProgramStarted()
        {
            Debug.Print("Program Started");
            button.ButtonPressed += button_ButtonPressed;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!runMotor)
            {
                // run motor
                runMotor = true;
                load.P1.Write(true);
            }
            else
            {
                // turn it off
                runMotor = false;
                load.P1.Write(false);
            }
        }
    }
}
