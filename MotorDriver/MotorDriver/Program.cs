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

namespace MotorDriver
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        private bool touch = false;
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");

            motorControllerL298.DebugPrintEnabled = true;

            Window window = display_T35.WPFWindow;
            Panel panel = new Panel();
            window.Child = panel;
            panel.TouchDown += panel_TouchDown;

              

        }

        void panel_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            if (!touch)
            {
                touch = true;
                motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 50, 1000);
                motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor2, 100, 1000);
            }
            else
            {
                motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 0 ,1000);
                motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor2, 0, 1000);
                touch = false;
            }

        }
    }
}
