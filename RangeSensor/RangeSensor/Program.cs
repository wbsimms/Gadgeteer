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
        private DigitalOutput output;
        private InterruptInput input;
        private static long beginTick, endTick, minTicks;
        private double inchConversion;

        void ProgramStarted()
        {
            minTicks = 6200L;
            inchConversion = 1440.0;

            Debug.Print("Program Started");
            output = extender.SetupDigitalOutput(GT.Socket.Pin.Seven, false);
            input = extender.SetupInterruptInput(GT.Socket.Pin.Three, GlitchFilterMode.On, ResistorMode.Disabled,
                                         InterruptMode.RisingAndFallingEdge);
            input.Interrupt += new InterruptInput.InterruptEventHandler(input_Interrupt);

            GT.Timer timer = new GT.Timer(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(GT.Timer timer)
        {
            output.Write(false);
            DelayMicroseconds(2);
            endTick = 0;
            beginTick = DateTime.Now.Ticks;
            output.Write(true);
            DelayMicroseconds(10);
            output.Write(false);
            DelayMicroseconds(2);
        }

        public double TicksToInches(long ticks)
        {
            return ticks / 148d;
        }

        void input_Interrupt(InterruptInput sender, bool value)
        {
            endTick = DateTime.Now.Ticks;
            long elapsed = endTick - beginTick;
            elapsed -= minTicks;
            double inches = TicksToInches(elapsed);
            Debug.Print("Inches : " + inches);
            Debug.Print("Start: " + beginTick);
            Debug.Print("End: " + endTick);
        }

        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

        private static void DelayMicroseconds(int microSeconds)
        {
            long stopTicks = Utility.GetMachineTime().Ticks +
                (microSeconds * TicksPerMicrosecond);
            while (Utility.GetMachineTime().Ticks < stopTicks) { }
        }

    }
}
