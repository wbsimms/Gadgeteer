using System;
using System.Threading;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;

namespace GadgeteerHelpers
{
    public class JoystickHelper
    {
        Joystick.Position lastJoystickPosition = new Joystick.Position();
        private Joystick joystick = null;
        public delegate void JoystickEventHander(object sender, JoystickEventsArgs args);
        public event JoystickEventHander JoystickMoved;
        private Thread joystickThread;
        private static bool runThread = false;

        public JoystickHelper(Joystick joystick)
        {
            this.joystick = joystick;
        }

        public void Start()
        {
            joystickThread = new Thread(WatchJoystick);
            runThread = true;
            joystickThread.Start();
        }

        public void Stop()
        {
            runThread = false;
            joystickThread.Join();
            joystickThread = null;
        }


        private void WatchJoystick()
        {
            while (true)
            {
                if (!runThread) return; 
                GetJoystickPosition();
                Thread.Sleep(100);
            }
        }


        protected virtual void OnJoystickMoved(JoystickEventsArgs args)
        {
            JoystickEventHander handler = JoystickMoved;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void GetJoystickPosition()
        {
            Joystick.Position position = joystick.GetJoystickPosition();
            if (DidJoystickMove(position))
            {
                OnJoystickMoved(new JoystickEventsArgs(position.X,position.Y));
            }
            lastJoystickPosition = position;
        }

        private bool DidJoystickMove(Joystick.Position position)
        {
            double realX = 0, realY = 0;
            Joystick.Position newJoystickPosition = position;
            double newX = newJoystickPosition.X - lastJoystickPosition.X;
            double newY = newJoystickPosition.Y - lastJoystickPosition.Y;

            // did we actually move...
            if (System.Math.Abs(newX) >= 0.03) { realX = newX; }
            if (System.Math.Abs(newY) >= 0.03) { realY = newY; }
            if (realX == 0.0 && realY == 0.0) return false;

            return true;
        }
    }

    public class JoystickEventsArgs : EventArgs
    {
        private double xPosition, yPosition;

        public JoystickEventsArgs(double x, double y)
        {
            this.xPosition = x;
            this.yPosition = y;
        }

        public double X
        {
            get { return this.xPosition; }
        }
        public double Y
        {
            get { return this.yPosition; }
        }
    }
}
