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
        private Joystick.Position currentPosition = new Joystick.Position();
        private uint displayHeight, displayWidth = 0;


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
            displayHeight = display_T35.Height;
            displayWidth = display_T35.Width;
            currentPosition.X = displayWidth/2.0;
            currentPosition.Y = displayHeight/2.0;
            MoveCursor();

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
                MoveCursor();
                Thread.Sleep(100);
            }
        }

        public void MoveCursor()
        {
            double realX = 0, realY = 0;
            Joystick.Position newJoystickPosition = joystick.GetJoystickPosition();
            double newX = joystickPosition.X-.5+currentPosition.X - currentPosition.X;
            double newY = joystickPosition.Y-.5+currentPosition.Y - currentPosition.Y;
            joystickPosition = newJoystickPosition;

            // did we actually move...
            if (System.Math.Abs(newX) >= 0.03){realX = newX;}
            if (System.Math.Abs(newY) >= 0.03){realY = newY;}
            if (realX == 0.0 && realY == 0.0) return;

            if (realX + currentPosition.X >= displayWidth) currentPosition.X = 0;
            if (realX + currentPosition.X <= 0) currentPosition.X = displayWidth;
            if (realY + currentPosition.Y >= displayHeight) currentPosition.Y = 0;
            if (realY + currentPosition.Y <= 0) currentPosition.Y = displayHeight;

            Debug.Print(realX + " " + realY);

            currentPosition.X += realX*5;
            currentPosition.Y += realY*5;
            display_T35.SimpleGraphics.DisplayEllipse(Color.White, (uint)currentPosition.X, (uint)currentPosition.Y, 2, 2);
        }

        public void DisplayText(string text)
        {
            if (lastHeight >= displayHeight)
            {
                display_T35.SimpleGraphics.Clear();
                lastHeight = 1;
            }
            display_T35.SimpleGraphics.DisplayText(text,f,Color.White,1,(uint)lastHeight);
            lastHeight += fontHeight;
            
        }

    }
}
