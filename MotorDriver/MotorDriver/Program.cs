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

            displayHelper = new SimpleDisplayHelper(display_T35,f);
            joystickHelper = new JoystickHelper(joystick);
            button.ButtonPressed += button_ButtonPressed;
            displayHelper.DisplayText("Program Started");

            joystickHelper.JoystickMoved += JoystickHelperJoystickMoved;
        }

        void JoystickHelperJoystickMoved(object sender, JoystickEventsArgs args)
        {
            displayHelper.DisplayText("X : "+args.X+ " -- Y : "+args.Y);
            if (args.Y < 0.6 && args.Y > 0.4)
            {
                motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 0, 100);
            }
            if (args.Y == 1.0) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 100, 1000);
                return;
            }
            if (args.Y == 1.0) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 100, 1000); return; }
            if (args.Y == -1.0) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, -100, 1000); return; }


            if (args.Y < 1.0 && args.Y >= 0.9) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 75, 500); return; }
            if (args.Y < 0.9 && args.Y >= 0.8) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 50, 500); return; }
            if (args.Y < 0.8 && args.Y >= 0.7) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 25, 500); return; }
            if (args.Y < 0.7 && args.Y >= 0.6) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, 10, 500); return; }
            if (args.Y < 0.4 && args.Y >= 0.3) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, -10, 500); return; }
            if (args.Y < 0.3 && args.Y >= 0.2) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, -25, 500); return; }
            if (args.Y < 0.2 && args.Y >= 0.1) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, -50, 500); return; }
            if (args.Y < 0.1 && args.Y >= 0.0) { motorControllerL298.MoveMotorRamp(MotorControllerL298.Motor.Motor1, -75, 500); return; }
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
