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
        private bool showKeypad = false;
        private KeyboardHelper keyboardHelper;
        private NumericKeypadHelper numericKeypad;
        void ProgramStarted()
        {
            keyboardHelper = new KeyboardHelper(display_T35,Resources.GetFont(Resources.FontResources.small));
            keyboardHelper.TextChanged += OnTextChanged;
            keyboardHelper.EnterPressed += OnEnterPressed;
            Debug.Print("Program Started");
            button.ButtonPressed += button_ButtonPressed;
        }

        void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            multicolorLed.BlinkOnce(GT.Color.Blue);
            String text = args.Text;
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!showKeypad)
            {
                keyboardHelper.TextChanged -= OnTextChanged;
                keyboardHelper.EnterPressed -= OnEnterPressed;

                numericKeypad = new NumericKeypadHelper(display_T35, Resources.GetFont(Resources.FontResources.small));
                numericKeypad.TextChanged += OnTextChanged;
                numericKeypad.EnterPressed += OnEnterPressed;
                showKeypad = true;
            }
            else
            {
                numericKeypad.TextChanged -= OnTextChanged;
                numericKeypad.EnterPressed -= OnEnterPressed;

                keyboardHelper = new KeyboardHelper(display_T35,Resources.GetFont(Resources.FontResources.small));
                keyboardHelper.TextChanged += OnTextChanged;
                keyboardHelper.EnterPressed += OnEnterPressed;
                showKeypad = false;
            }
        }

        private void OnEnterPressed(object sender, EnterPressedEventArgs args)
        {
            Debug.Print("Enter pressed with value = "+args.Text);
        }
    }
}
