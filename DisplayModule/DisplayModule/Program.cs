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

namespace DisplayModule
{

    public partial class Program
    {
        public Queue multiLedColors = new Queue();
        private GT.Timer ledTimer;
        private uint displayHeight;
        private uint fontHeight;
        private uint currentHeight = 0;
        private Font f;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            multiLedColors.Enqueue(GT.Color.White);
            multiLedColors.Enqueue(GT.Color.Blue);
            multiLedColors.Enqueue(GT.Color.Red);
            multiLedColors.Enqueue(GT.Color.Green);
            multicolorLed.TurnOff();

            ledTimer = new GT.Timer(1000);
            ledTimer.Tick += ledTimer_Tick;

            f = Resources.GetFont(Resources.FontResources.NinaB);
            fontHeight = (uint)f.Height;
            displayHeight = display_T35.Height;

            button.ButtonPressed += button_ButtonPressed;
        }

        void ledTimer_Tick(GT.Timer timer)
        {
            multicolorLed.TurnColor((GT.Color)multiLedColors.Dequeue());
            multiLedColors.Enqueue(multicolorLed.GetCurrentColor());
            String colorString = "";
            if (multicolorLed.GetCurrentColor() == GT.Color.Green) colorString = "Green";
            if (multicolorLed.GetCurrentColor() == GT.Color.White) colorString = "White";
            if (multicolorLed.GetCurrentColor() == GT.Color.Blue) colorString = "Blue";
            if (multicolorLed.GetCurrentColor() == GT.Color.Red) colorString = "Red";
            DisplaySimpleText("Changing to : " + colorString);
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            DisplaySimpleText("Button Pressed");
            if (!button.IsLedOn)
            {   
                button.TurnLEDOn();
                ledTimer.Start();
            }
            else
            {
                button.TurnLEDOff();
                multicolorLed.TurnOff();
                ledTimer.Stop();
            }
        }

        private void DisplaySimpleText(String text)
        {
            if (currentHeight >= displayHeight)
            {
                display_T35.SimpleGraphics.Clear();
                currentHeight = 0;

            }
            display_T35.SimpleGraphics.DisplayText(text,f,Color.White,0,currentHeight);
            currentHeight += fontHeight;
        }
    }
}
