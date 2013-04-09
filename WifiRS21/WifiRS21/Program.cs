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
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");

            if (!wifi_RS21.Interface.IsOpen)
            {
                wifi_RS21.Interface.Open();
            }
            if (!wifi_RS21.Interface.NetworkInterface.IsDhcpEnabled)
            {
                wifi_RS21.Interface.NetworkInterface.EnableDhcp();
            }

            NetworkInterfaceExtension.AssignNetworkingStackTo(wifi_RS21.Interface);

            WiFiNetworkInfo[] scanResponse = wifi_RS21.Interface.Scan("yournetwork");
            if (scanResponse != null)
            {
                wifi_RS21.Interface.Join(scanResponse[0],"yourkey");
            }

            HttpRequest request = WebClient.GetFromWeb("http://wbsimms.com");
            request.ResponseReceived += new HttpRequest.ResponseHandler(request_ResponseReceived);

        }

        void request_ResponseReceived(HttpRequest sender, HttpResponse response)
        {
            string text = response.Text;
            Debug.Print(text);
        }
    }
}
