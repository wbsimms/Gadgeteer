using System;
using System.Collections;
using System.Threading;
using GHI.Premium.Net;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace WifiRS21
{
    public partial class Program
    {
        Font ninaB = Resources.GetFont(Resources.FontResources.NinaB);
        private uint currentY = 1;
        private string ipAddress;
        private bool serverStarted = false;
        private string SSID = "";
        private string PASSWORD = "";
        void ProgramStarted()
        {
            display_T35.SimpleGraphics.DisplayText("Press Button to load page", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;
            button.ButtonPressed += new Button.ButtonEventHandler(button_ButtonPressed);
        }

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            display_T35.SimpleGraphics.DisplayText("Button Pressed.....", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;

            if (!serverStarted)
                StartServer();
            else
                StopServer();
        }

        private void StopServer()
        {
            WebServer.StopLocalServer();
            serverStarted = false;
            display_T35.SimpleGraphics.DisplayText("Server Stopped", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;
        }

        private void StartServer()
        {
            if (!wifi_RS21.Interface.IsOpen)
            {
                wifi_RS21.Interface.Open();
            }
            if (!wifi_RS21.Interface.NetworkInterface.IsDhcpEnabled)
            {
                wifi_RS21.Interface.NetworkInterface.EnableDhcp();
            }

            if (!wifi_RS21.Interface.IsLinkConnected)
            {
                wifi_RS21.Interface.NetworkAddressChanged += new NetworkInterfaceExtension.NetworkAddressChangedEventHandler(Interface_NetworkAddressChanged);
                NetworkInterfaceExtension.AssignNetworkingStackTo(wifi_RS21.Interface);
                WiFiNetworkInfo[] scanResponse = wifi_RS21.Interface.Scan(SSID);
                if (scanResponse != null)
                {
                    wifi_RS21.Interface.Join(scanResponse[0], PASSWORD);
                }
            }
            else
            {
                RunServer();
            }
        }

        void Interface_NetworkAddressChanged(object sender, EventArgs e)
        {
            ipAddress = wifi_RS21.Interface.NetworkInterface.IPAddress;
            display_T35.SimpleGraphics.DisplayText("Received IP Address : "+ipAddress, ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;
            RunServer();
        }

        public void RunServer()
        {
            display_T35.SimpleGraphics.DisplayText("Starting Server", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;

            WebEvent GetValueEvent = WebServer.SetupWebEvent("GetValue");
            GetValueEvent.WebEventReceived += new WebEvent.ReceivedWebEventHandler(GetValueEvent_WebEventReceived);

            WebServer.StartLocalServer(ipAddress, 80);
            serverStarted = true;
        }

        void GetValueEvent_WebEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            display_T35.SimpleGraphics.DisplayText("Received Request...", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;
            
            responder.Respond("testing");

            display_T35.SimpleGraphics.DisplayText("Sending response...", ninaB, Color.White, 1, currentY);
            currentY += (uint)ninaB.Height;

        }
    }
}
