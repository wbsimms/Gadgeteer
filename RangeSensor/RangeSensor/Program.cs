using System;
using System.Collections;
using System.Threading;
using Gadgeteer.Interfaces;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace RangeSensor
{
    public partial class Program
    {
        private DigitalOutput trigger;
        private DigitalInput echo;
        public int AcceptableErrorRate = 10;
        private DigitalInput Echo;
        private const int MAX_DISTANCE = 400;
        private const int MaxFlag = -1;
        private const int MIN_DISTANCE = 2;
        private const int MinFlag = -2;
        public readonly int SENSOR_ERROR = -1;
        private readonly int TicksPerMicrosecond = (int)TimeSpan.TicksPerMillisecond/1000;
        private DigitalOutput Trigger;
        GT.Timer timer = new GT.Timer(1000);
        private bool timerStarted = false;

        void ProgramStarted()
        {
            Debug.Print("Program Started");
            trigger = extender.SetupDigitalOutput(GT.Socket.Pin.Four, false);

            echo = extender.SetupDigitalInput(GT.Socket.Pin.Three, GlitchFilterMode.Off, ResistorMode.Disabled);

            timer.Tick += timer_Tick;
            button.ButtonPressed += button_ButtonPressed;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!timerStarted)
            {
                timer.Start();
                timerStarted = true;
            }
            else
            {
                timer.Stop();
                timerStarted = false;
            }
        }

        void timer_Tick(GT.Timer timer)
        {
            int distance = GetDistanceInCentimeters(5);
            Debug.Print("Mine : "+distance.ToString());
            Debug.Print("Theirs : " + distance_US3.GetDistanceInCentimeters(5));
        }

        public int GetDistanceInCentimeters(int numMeasurements = 1)
        {
            int distanceHelper = 0;
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; i < numMeasurements; i++)
            {
                distanceHelper = this.GetDistanceHelper();
                if ((distanceHelper != -1) || (distanceHelper != -2))
                {
                    num2 += distanceHelper;
                }
                else
                {
                    num3++;
                    i--;
                    if (num3 > this.AcceptableErrorRate)
                    {
                        return this.SENSOR_ERROR;
                    }
                }
            }
            return (num2 / numMeasurements);
        }

        private int GetDistanceHelper()
        {
            long ticks = 0L;
            int num2 = 0;
            long num3 = 0L;
            int num4 = 0;
            this.trigger.Write(true);
            Thread.Sleep(10);
            this.trigger.Write(false);
            int num5 = 0;
            while (!this.echo.Read())
            {
                num5++;
                if (num5 > 0x3e8)
                {
                    break;
                }
                Thread.Sleep(0);
            }
            ticks = DateTime.Now.Ticks;
            while (this.echo.Read())
            {
                Thread.Sleep(0);
            }
            num3 = DateTime.Now.Ticks - ticks;
            num2 = ((int)num3) / this.TicksPerMicrosecond;
            num4 = num2 / 0x3a;
            num4 += 2;
            if (num4 >= 400)
            {
                return -1;
            }
            if (num4 >= 2)
            {
                return num4;
            }
            return -2;
        }

    }
}
