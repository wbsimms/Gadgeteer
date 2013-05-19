using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace DisplayModuleTouch
{

    public partial class Program
    {
        private Font f;

        void ProgramStarted()
        {
            f = Resources.GetFont(Resources.FontResources.NinaB);

            Window window = display_T35.WPFWindow;
            window.Background = new SolidColorBrush(Color.Black);
            Panel p = new Panel();
            window.Child = p;

            StackPanel controls = new StackPanel(Orientation.Vertical);
            controls.SetMargin(10);
            p.Children.Add(controls);

            String red = "Red";
            String blue = "Blue";
            String green = "Green";
            String white = "White";
            String off = "Off";
            Border redBorder = GetBorder(GT.Color.Red);
            Border blueBorder = GetBorder(GT.Color.Blue);
            Border greenBorder = GetBorder(GT.Color.Green);
            Border whiteBorder = GetBorder(GT.Color.White, GT.Color.Black);
            Border offBorder = GetBorder(GT.Color.DarkGray, Color.Black);

            redBorder.TouchDown += redText_TouchDown;
            blueBorder.TouchDown += blueText_TouchDown;
            greenBorder.TouchDown += greenText_TouchDown;
            whiteBorder.TouchDown += whiteText_TouchDown;
            offBorder.TouchDown += TurnOff;
            offBorder.TouchUp += TurnOff;


            controls.Children.Add(GetStackPanel(red,redBorder));
            controls.Children.Add(GetStackPanel(green,greenBorder));
            controls.Children.Add(GetStackPanel(blue,blueBorder));
            controls.Children.Add(GetStackPanel(white,whiteBorder));
            controls.Children.Add(GetStackPanel(off,offBorder));

        }

        public StackPanel GetStackPanel(String text, Border border)
        {
            StackPanel sp = new StackPanel(Orientation.Horizontal);
            sp.SetMargin(10);
            sp.HorizontalAlignment = HorizontalAlignment.Stretch;
            sp.VerticalAlignment = VerticalAlignment.Top;
            sp.Children.Add(new Text(f, text) { ForeColor = Color.White, Width = 60, Height = border.Height, VerticalAlignment = VerticalAlignment.Center });
            sp.Children.Add(border);
            return sp;
        }

        void TurnOff(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            multicolorLed.TurnOff();
        }

        void redText_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            multicolorLed.TurnRed();
        }

        void blueText_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            multicolorLed.TurnBlue();
        }

        void greenText_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            multicolorLed.TurnGreen();
        }

        void whiteText_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            multicolorLed.TurnWhite();
        }

        public Border GetBorder(Color color, Color foreground = Color.White)
        {
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(color);
            border.Height = f.Height + 5;
            border.Width = 300;
            return border;
        }

    }
}
