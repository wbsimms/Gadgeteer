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
using GadgeteerHelpers;

namespace MusicalDistanceSensor
{
    public partial class Program
    {
        private SimpleDisplayHelper display;
        private bool started;
        private GT.Timer programTimer = new GT.Timer(500);
        private PWMOutput pwm;
        Queue colors = new Queue();
        private uint lastTone = 0;

        void ProgramStarted()
        {
            colors.Enqueue(GT.Color.Blue);
            colors.Enqueue(GT.Color.Green);
            colors.Enqueue(GT.Color.Red);
            colors.Enqueue(GT.Color.White);
            button.ButtonPressed += button_ButtonPressed;
            programTimer.Tick += programTimer_Tick;
            display = new SimpleDisplayHelper(display_T35,Resources.GetFont(Resources.FontResources.small));
            pwm = extender.SetupPWMOutput(GT.Socket.Pin.Nine);
            display.DisplayText("Program Started");
        }

        void programTimer_Tick(GT.Timer timer)
        {
            int distance = distance_US3.GetDistanceInCentimeters(20);
            PlayNote(distance);
        }

        private void PlayNote(int distance)
        {
            // timeHigh = 1/(2 * toneFrequency) = period / 2
            /*
             note         frequency
             c            261 Hz   
             c#			  277		
             d            294 Hz   
             d#			  311		
             e            329 Hz   
             f            349 Hz   
             f#			  370		
             g            392 Hz   
             g#			  415
             a            440 Hz   
             a#			  466
             b            493 Hz   
             */
            uint tone = 2272; // A
            if (distance < 115 && distance >= 105) tone = 3830; // whole
            if (distance < 105 && distance >= 95) tone = 3610; // whole
            if (distance < 95 && distance >= 85) tone = 3400; // whole
            if (distance < 85 && distance >= 75) tone = 3216; // whole
            if (distance < 75 && distance >= 65) tone = 3038; // half
            if (distance < 65 && distance >= 55) tone = 2864; // whole
            if (distance < 55 && distance >= 45) tone = 2702; // whole
            if (distance < 45 && distance >= 35) tone = 2550; // whole
            if (distance < 35 && distance >= 25) tone = 2410; // whole
            if (distance < 25 && distance >= 15) tone = 2272; // whole
            if (distance < 15 && distance >= 8) tone = 2146; // whole
            if (distance < 8 && distance >= 0) tone = 2028; // half

            if (tone == lastTone) return;
            GT.Color c = (GT.Color)colors.Dequeue();
            multicolorLed.TurnColor(c);
            colors.Enqueue(c);
            pwm.SetPulse(tone * 1000, (tone / 2) * 1000);
            lastTone = tone;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!started)
            {
                started = true;
                pwm.Active = true;
                programTimer.Start();
            }
            else
            {
                started = false;
                pwm.Active = false;
                programTimer.Stop();
            }
        }
    }
}
