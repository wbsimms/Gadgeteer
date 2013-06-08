using System;
using System.Threading;
using Gadgeteer.Interfaces;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace RangeSensor
{
    public partial class Program
    {
        public int AcceptableErrorRate = 10;
        public readonly int SENSOR_ERROR = -1;
        GT.Timer timer = new GT.Timer(1000);
        private bool timerStarted = false;
        private Font f;
        private uint fontHeight;
        private uint displayHeight;
        uint currentHeight;

        void ProgramStarted()
        {
            f = Resources.GetFont(Resources.FontResources.NinaB);
            fontHeight = (uint)f.Height;
            displayHeight = display_T35.Height;
            currentHeight = 0;

            Debug.Print("Program Started");
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
            DisplaySimpleText("Theirs : " + distance_US3.GetDistanceInCentimeters(5));
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
