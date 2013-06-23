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
        private bool touch = false;
        void ProgramStarted()
        {
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
