using System;
using System.Collections;
using System.Threading;
using Gadgeteer.Interfaces;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace ServoControl
{
    public partial class Program
    {
        private static bool isActive = false;
        private static PWMOutput pwmOutput;
        private Thread servoThread;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            Debug.Print("Program Started");
            pwmOutput = extender.SetupPWMOutput(GT.Socket.Pin.Nine);
            button.ButtonPressed += button_ButtonPressed;
        }

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            isActive = !isActive;
            if (isActive)
            {
                servoThread = new Thread(RunServo);
                servoThread.Start();
            }
            else
            {
                servoThread.Join();
            }
        }

        static void RunServo()
        {
            int currentPosition = 0;
            pwmOutput.Active = true;
            while (true)
            {
                if (!isActive)
                {
                    pwmOutput.Active = false;
                    return;
                }
                currentPosition = MoveServo(pwmOutput,2400, 1500); // we're not sure where the servo might be... assume it's at the max position
                currentPosition = MoveServo(pwmOutput, currentPosition, 650);
                currentPosition = MoveServo(pwmOutput, currentPosition, 2350);
                for (UInt32 hightime = 650; hightime <= 2350; hightime += 100)
                {
                    currentPosition = MoveServo(pwmOutput, currentPosition, hightime);
                }
            }
        }

        static int MoveServo(PWMOutput server, int currentPosition, uint targetPosition)
        {
            int milliSecondsPerDegreeWait = 4; // 3.333 really...
            pwmOutput.SetPulse(20 * 1000000, targetPosition * 1000);
            Thread.Sleep((System.Math.Abs((currentPosition - (int)targetPosition))/10) * milliSecondsPerDegreeWait);
            return (int)targetPosition;
        }
    }
}
