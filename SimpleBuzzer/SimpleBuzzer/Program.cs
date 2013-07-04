using System;
using System.Collections;
using System.Threading;
using Gadgeteer.Interfaces;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace SimpleBuzzer
{
    public partial class Program
    {
        private PWMOutput pwm;

        void ProgramStarted()
        {
            pwm = extender.SetupPWMOutput(GT.Socket.Pin.Nine);
            display_T35.WPFWindow.Child = GetPanel();
            display_T35.WPFWindow.Background = new SolidColorBrush(GT.Color.DarkGray);
            Debug.Print("Program Started");

        }

        private Panel GetPanel()
        {
            Panel mainPanel = new Panel();
            StackPanel sp = new StackPanel(Orientation.Horizontal);
            mainPanel.Children.Add(sp);

            Border c = GetBorder("C");
            Border d = GetBorder("D");
            Border e = GetBorder("E");
            Border f = GetBorder("F");
            Border g = GetBorder("G");
            Border a = GetBorder("A");
            Border b = GetBorder("B");
            sp.Children.Add(c);
            sp.Children.Add(d);
            sp.Children.Add(e);
            sp.Children.Add(f);
            sp.Children.Add(g);
            sp.Children.Add(a);
            sp.Children.Add(b);
            return mainPanel;
        }

        void TouchDown(object sender, TouchEventArgs e)
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
            Border b = (Border) sender;
            Text t = (Text) b.Child;
            uint tone = 2272; // A
            if (t.TextContent == "C") tone = 3830; // whole
            if (t.TextContent == "C#") tone = 3610; // whole
            if (t.TextContent == "D") tone = 3400; // whole
            if (t.TextContent == "D#") tone = 3216; // whole
            if (t.TextContent == "E") tone = 3038; // half
            if (t.TextContent == "F") tone = 2864; // whole
            if (t.TextContent == "F#") tone = 2702; // whole
            if (t.TextContent == "G") tone = 2550; // whole
            if (t.TextContent == "G#") tone = 2410; // whole
            if (t.TextContent == "A") tone = 2272; // whole
            if (t.TextContent == "A#") tone = 2146; // whole
            if (t.TextContent == "B") tone = 2028; // half
            
            pwm.Active = true;
            pwm.SetPulse(tone * 1000, (tone / 2) * 1000);
        }

        private void TouchUp(object sender, TouchEventArgs e)
        {
            pwm.Active = false;
        }

        public Border GetBorder(String text)
        {
            Font f = Resources.GetFont(Resources.FontResources.small);
            Border retval = new Border();
            retval.Background = new SolidColorBrush(GT.Color.White);
            retval.BorderBrush = new SolidColorBrush(GT.Color.Black);
            retval.Foreground = new SolidColorBrush(GT.Color.Black);
            retval.Height = 35;
            retval.Width = 35;
            retval.SetMargin(4,4,4,4);
            Text t = new Text(f,text);
            t.ForeColor = GT.Color.Black;
            retval.Child = t;
            retval.TouchDown += TouchDown;
            retval.TouchUp += TouchUp;
            return retval;
        }
    }
}
