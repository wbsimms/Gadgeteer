using System;
using System.Collections;
using System.Text;
using System.Threading;
using GHI.Premium.Net;
using GadgeteerHelpers;
using Microsoft.SPOT;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace DistanceLoggerWithWebReporting
{
    public partial class Program
    {
        private bool started = false;
        private GT.Timer distanceThread;
        private IDictionary dictionary = new Hashtable();

        void ProgramStarted()
        {
            button.ButtonPressed += button_ButtonPressed;
            distanceThread = new GT.Timer(500);
            distanceThread.Tick += distanceThread_Tick;
            Debug.Print("Program Started");
        }

        void distanceThread_Tick(GT.Timer timer)
        {
            int distance = distance_US3.GetDistanceInCentimeters(10);
            if (distance < 50)
            {
                DateTime now = DateTime.Now;
                Debug.Print("At "+now+" distance recorded : " + distance);
                multicolorLed.BlinkOnce(GT.Color.Green,new TimeSpan(0,0,0,1));
                dictionary.Add(DateTime.Now, distance);
            }
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            if (!started)
            {
                dictionary.Clear();
                started = true;
                WiFiRS21Helper helper = new WiFiRS21Helper(wifi_RS21, "YOURSSID", "YOURPASSWORD");
                helper.StartWiFiServer();
                RunServer(helper.IpAddress);
                distanceThread.Start();
            }
            else
            {
                started = false;
                distanceThread.Stop();
                StopServer();
            }
        }

        private void StopServer()
        {
            WebServer.StopLocalServer();
        }

        public void RunServer(string ipAddress)
        {
            WebEvent GetValueEvent = WebServer.SetupWebEvent("GetLogs");
            GetValueEvent.WebEventReceived += GetValueEvent_WebEventReceived;
            WebServer.StartLocalServer(ipAddress, 80);
        }


        void GetValueEvent_WebEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var date in dictionary.Keys)
            {
                sb.AppendLine(date + " : " + dictionary[date]);
            }
            responder.Respond(sb.ToString());
        }
    }
}
