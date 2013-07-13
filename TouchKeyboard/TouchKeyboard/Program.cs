using System;
using System.Collections;
using System.Threading;
using GadgeteerHelper;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace TouchKeyboard
{
    public partial class Program
    {
        void ProgramStarted()
        {
            KeyboardHelper keyboardHelper = new KeyboardHelper(display_T35,Resources.GetFont(Resources.FontResources.small));
            Debug.Print("Program Started");
            button.ButtonPressed += button_ButtonPressed;
            keyboardHelper.TextChanged += keyboardHelper_TextChanged;
        }

        void keyboardHelper_TextChanged(object sender, TextChangedEventArgs args)
        {
            multicolorLed.BlinkOnce(GT.Color.Blue);
            String text = args.Text;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            multicolorLed.BlinkOnce(GT.Color.Green);
        }

    }
}
