using System;
using System.Collections;
using System.Text;
using GHI.Premium.Net;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using Microsoft.SPOT;

namespace GadgeteerHelpers
{
    public class WiFiRS21Helper
    {
        private WiFi_RS21 wifi_RS21;
        private string ipAddress;
        private string SSID = "";
        private string PASSWORD = "";


        public WiFiRS21Helper(WiFi_RS21 wifi, string ssid, string password)
        {
            wifi_RS21 = wifi;
            this.SSID = ssid;
            this.PASSWORD = password;
        }

        public string IpAddress
        {
            get { return ipAddress; }
        }


        private void InterfaceOnNetworkAddressChanged(object sender, EventArgs eventArgs)
        {
            ipAddress = wifi_RS21.Interface.NetworkInterface.IPAddress;
        }

        public void StartWiFiServer()
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
                wifi_RS21.Interface.NetworkAddressChanged += InterfaceOnNetworkAddressChanged;
                NetworkInterfaceExtension.AssignNetworkingStackTo(wifi_RS21.Interface);
                WiFiNetworkInfo[] scanResponse = wifi_RS21.Interface.Scan(SSID);
                if (scanResponse != null)
                {
                    wifi_RS21.Interface.Join(scanResponse[0], PASSWORD);
                }
            }
            while (ipAddress == null)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }

}
