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
using Gadgeteer.Modules.GHIElectronics;

namespace PIRExample
{
    public partial class Program
    {
        private static int counter = 0;
        // This method is run when the mainboard is powered up or reset.   
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
            motion_Sensor.Motion_Sensed += new Motion_Sensor.Motion_SensorEventHandler(motion_Sensor_Motion_Sensed);

            this.display_T35.SimpleGraphics.DisplayText("Testing working",Resources.GetFont(Resources.FontResources.NinaB),Color.White,10,10);
        }

        void motion_Sensor_Motion_Sensed(Motion_Sensor sender, Motion_Sensor.Motion_SensorState state)
        {
//            display_T35.SimpleGraphics.DisplayText("Motion Sensed", Resources.GetFont(Resources.FontResources.NinaB), Color.White, 10, 10);
            Debug.Print("Motion Detected! "+counter++);
        }
    }
}
