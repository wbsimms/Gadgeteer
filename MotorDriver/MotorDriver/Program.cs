using Gadgeteer.Modules.GHIElectronics;
using GadgeteerHelpers;
using Microsoft.SPOT;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace MotorDriver
{
    public partial class Program
    {
        private bool motorEnabled = false;
        private SimpleDisplayHelper displayHelper;
        private JoystickHelper joystickHelper;
        private Font f = Resources.GetFont(Resources.FontResources.NinaB);

        void ProgramStarted()
        {
            motorControllerL298.DebugPrintEnabled = true;

            displayHelper = new SimpleDisplayHelper(display_T35,f.Height);
            joystickHelper = new JoystickHelper(joystick);
            button.ButtonPressed += button_ButtonPressed;
            displayHelper.DisplayText("Program Started");


            joystickHelper.JoystickMoved += JoystickHelperJoystickMoved;
        }

        void JoystickHelperJoystickMoved(object sender, JoystickEventsArgs args)
        {
            displayHelper.DisplayText("X : "+args.X+ " -- Y : "+args.Y);
        }


        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            displayHelper.DisplayText("Button Pressed");
            if (!motorEnabled)
            {
                motorEnabled = true;
                joystickHelper.Start();
            }
            else
            {
                motorEnabled = false;
                joystickHelper.Stop();
            }
        }
    }
}
