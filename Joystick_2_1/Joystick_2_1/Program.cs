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

namespace Joystick_2_1
{
    public partial class Program
    {
        Joystick.Position joystickPosition = new Joystick.Position();
        Thread joystickThread = null;
        private static bool runJoystickThread = true;
        private Font f = null;
        private int fontHeight = 0;
        private int lastHeight = 1;
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
            f = Resources.GetFont(Resources.FontResources.NinaB);
            fontHeight = f.Height;
            joystickPosition = joystick.GetJoystickPosition();
            joystickThread = new Thread(JoystickReadThread);
            joystickThread.Start();
            button.ButtonPressed += button_ButtonPressed;
        }

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            runJoystickThread = false;
        }

        public void JoystickReadThread()
        {
            while (true)
            {
                if (runJoystickThread == false) return;
                Joystick.Position newJoystickPosition = joystick.GetJoystickPosition();
                double newX = System.Math.Abs(joystickPosition.X - newJoystickPosition.X);
                double newY = System.Math.Abs(joystickPosition.Y - newJoystickPosition.Y);
                if (newX >= 0.02 || newY >= 0.02)
                    DisplayText(joystickPosition.X + " " + joystickPosition.Y);
                joystickPosition = newJoystickPosition;
                Thread.Sleep(100);
            }
        }

        public void DisplayText(string text)
        {
            if (lastHeight >= display_T35.Height)
            {
                display_T35.SimpleGraphics.Clear();
                lastHeight = 1;
            }
            display_T35.SimpleGraphics.DisplayText(text,f,Color.White,1,(uint)lastHeight);
            lastHeight += fontHeight;
            
        }

    }
}
