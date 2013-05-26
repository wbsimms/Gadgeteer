using System;
using System.Collections;
using System.Threading;
using Gadgeteer.Interfaces;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace JoystickTouchServoControl
{

    public partial class Program
    {
        private const int servoLimitLow = 650, servoLimitHigh = 2350;
        const int currentServoPosition = 2400;
        Joystick.Position lastJoystickPosition = new Joystick.Position();
        public static PWMOutput pwmOutput = null;
        public static bool isActive = false;
        private Font f;
        private uint fontHeight;
        private uint displayHeight;
        private uint currentHeight;
        // This method is run when the mainboard is powered up or reset.   
        private void ProgramStarted()
        {
            f = Resources.GetFont(Resources.FontResources.NinaB);
            fontHeight = (uint)f.Height;
            displayHeight = display_T35.Height;
            currentHeight = 0;
            pwmOutput = extender.SetupPWMOutput(GT.Socket.Pin.Nine);

            GT.Timer jt = new GT.Timer(100);
            jt.Tick += jt_Tick;
            jt.Start();

            Debug.Print("Program Started");
        }

        void jt_Tick(GT.Timer timer)
        {
            Joystick.Position position = joystick.GetJoystickPosition();

            if (didJoystickMove(position))
            {
                DisplaySimpleText("Position : " + position.X + " - " + position.Y);
                int range = servoLimitHigh - servoLimitLow;
                int stepsServo = (int)range/10;
                if (position.X > lastJoystickPosition.X)
                {
                    // move right
                    DisplaySimpleText("Move right");
                }
                else
                {
                    // move left
                    DisplaySimpleText("Move left");
                }
                if (position.X <= .1) MoveServo(servoLimitLow);
                if (position.X > .1 && position.X <= .2) MoveServo(servoLimitLow + stepsServo);
                if (position.X > .2 && position.X <= .3) MoveServo(servoLimitLow + (stepsServo * 2));
                if (position.X > .3 && position.X <= .4) MoveServo(servoLimitLow + (stepsServo * 3));
                if (position.X > .4 && position.X <= .5) MoveServo(servoLimitLow + (stepsServo * 4));
                if (position.X > .5 && position.X <= .6) MoveServo(servoLimitLow + (stepsServo * 5));
                if (position.X > .6 && position.X <= .7) MoveServo(servoLimitLow + (stepsServo * 6));
                if (position.X > .7 && position.X <= .8) MoveServo(servoLimitLow + (stepsServo * 7));
                if (position.X > .8 && position.X <= .9) MoveServo(servoLimitLow + (stepsServo * 8));
                if (position.X >= .9) MoveServo(servoLimitHigh);

                lastJoystickPosition = position;
            }
        }

        private bool didJoystickMove(Joystick.Position position)
        {
            double realX = 0, realY = 0;
            Joystick.Position newJoystickPosition = position;
            double newX = newJoystickPosition.X - lastJoystickPosition.X;
            double newY = newJoystickPosition.Y - lastJoystickPosition.Y;

            // did we actually move...
            if (System.Math.Abs(newX) >= 0.03) { realX = newX; }
            if (System.Math.Abs(newY) >= 0.03) { realY = newY; }
            if (realX == 0.0 && realY == 0.0) return false;

            return true;
        }

        static int MoveServo(int targetPosition)
        {
            int milliSecondsPerDegreeWait = 4; // 3.333 really...
            pwmOutput.SetPulse(20 * 1000000, (uint)targetPosition * 1000);
            Thread.Sleep((System.Math.Abs((currentServoPosition - targetPosition)) / 10) * milliSecondsPerDegreeWait);
            return (int)targetPosition;
        }

        private void DisplaySimpleText(String text)
        {
            if (currentHeight >= displayHeight)
            {
                display_T35.SimpleGraphics.Clear();
                currentHeight = 0;

            }
            display_T35.SimpleGraphics.DisplayText(text, f, Color.White, 0, currentHeight);
            currentHeight += fontHeight;
        }
    }

}
