//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServoControl {
    using Gadgeteer;
    using GTM = Gadgeteer.Modules;
    
    
    public partial class Program : Gadgeteer.Program {
        
        private Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClientDP;
        
        private Gadgeteer.Modules.GHIElectronics.Extender extender;
        
        private Gadgeteer.Modules.GHIElectronics.Button button;
        
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
            this.button = new GTM.GHIElectronics.Button(4);
            this.extender = new GTM.GHIElectronics.Extender(11);
        }
    }
}
