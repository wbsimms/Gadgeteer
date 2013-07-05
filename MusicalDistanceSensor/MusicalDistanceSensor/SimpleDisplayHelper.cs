using System;
using System.Resources;
using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;

namespace GadgeteerHelpers
{

    public class SimpleDisplayHelper
    {
        private Font f = null;
        private int fontHeight = 0;
        private int lastHeight = 1;
        private Display_T35 display_T35 = null;
        private uint displayHeight;

        public SimpleDisplayHelper(Display_T35 display_t35, Font font)
        {
            this.display_T35 = display_t35;
            displayHeight = display_T35.Height;
            this.fontHeight = font.Height;
            this.f = font;
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
