using System;
using System.Resources;
using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;

namespace WBSimms.Com
{

    public class SimpleDisplayHelper
    {
        private Font f = null;
        private int fontHeight = 0;
        private int lastHeight = 1;
        private Display_T35 display_T35 = null;
        private uint displayHeight;

        public SimpleDisplayHelper(Display_T35 display_t35, int fontHeight)
        {
            this.display_T35 = display_t35;
            displayHeight = display_T35.Height;
            this.fontHeight = fontHeight;
        }

        public void DisplayText(string text)
        {
            if (lastHeight >= displayHeight)
            {
                display_T35.SimpleGraphics.Clear();
                lastHeight = 1;
            }
            display_T35.SimpleGraphics.DisplayText(text, f, Color.White, 1, (uint) lastHeight);
            lastHeight += fontHeight;

        }
    }
}
