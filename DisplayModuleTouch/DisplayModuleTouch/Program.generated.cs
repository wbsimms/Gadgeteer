//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DisplayModuleTouch {
    using Gadgeteer;
    using GTM = Gadgeteer.Modules;
    
    
    public partial class Program : Gadgeteer.Program {
        
        private Gadgeteer.Modules.GHIElectronics.Display_T35 display_T35;
        
        private Gadgeteer.Modules.GHIElectronics.MulticolorLed multicolorLed;
        
        private Gadgeteer.Modules.GHIElectronics.Button button;
        
        private Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClientDP;
        
        public static void Main() {
            // Important to initialize the Mainboard first
            Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
            Program p = new Program();
            p.InitializeModules();
            p.ProgramStarted();
            // Starts Dispatcher
            p.Run();
        }
        
        private void InitializeModules() {
            this.usbClientDP = new GTM.GHIElectronics.UsbClientDP(1);
            this.multicolorLed = new GTM.GHIElectronics.MulticolorLed(4);
            this.button = new GTM.GHIElectronics.Button(8);
            this.display_T35 = new GTM.GHIElectronics.Display_T35(14, 13, 12, 10);
        }
    }
}
