using System;
using System.Collections;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using TouchKeyboard;

namespace GadgeteerHelper
{
    public class NumericKeypadHelper
    {
        private Display_T35 display;
        private Font font;
        private Text displayText;
        private IList keys = new ArrayList();
        private int margin = 12;

        public delegate void TextChangedEventHander(object sender, TextChangedEventArgs args);
        public event TextChangedEventHander TextChanged;

        public delegate void EnterPressedEventHander(object sender, EnterPressedEventArgs args);
        public event EnterPressedEventHander EnterPressed;

        StackPanel spacer = new StackPanel(Orientation.Horizontal);
        StackPanel spacer1 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow1 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow2 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow3 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow4 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow5 = new StackPanel(Orientation.Horizontal);

        public NumericKeypadHelper(Display_T35 display, Font font)
        {
            this.display = display;
            this.font = font;
            this.displayText = new Text(font,"");
            spacer.Height = 2;
            spacer1.Height = 2;
            Init();

        }

        public void Init()
        {
            Window window = display.WPFWindow;
            Panel mainPanel = new Panel();
            window.Child = mainPanel;
            StackPanel sp = new StackPanel(Orientation.Vertical);
            mainPanel.Children.Add(sp);

            sp.Children.Add(spacer);
            StackPanel dispalySP = new StackPanel(Orientation.Horizontal);
            sp.Children.Add(dispalySP);
            dispalySP.Children.Add(displayText);

            sp.Children.Add(spacer1);
            sp.Children.Add(keysRow1);
            sp.Children.Add(keysRow2);
            sp.Children.Add(keysRow3);
            sp.Children.Add(keysRow4);
            sp.Children.Add(keysRow5);
            keysRow1.HorizontalAlignment = HorizontalAlignment.Center;
            keysRow2.HorizontalAlignment = HorizontalAlignment.Center;
            keysRow3.HorizontalAlignment = HorizontalAlignment.Center;
            keysRow4.HorizontalAlignment = HorizontalAlignment.Center;
            keysRow5.HorizontalAlignment = HorizontalAlignment.Center;

            UnShiftKeys(false);

            Key delKey = new Key(font, "Delete");
            delKey.keyPressedHandler += keyPressedHandler;
            keysRow5.Children.Add(delKey.RenderKey());
            Key enterKey = new Key(font, "Enter");
            enterKey.keyPressedHandler += keyPressedHandler;
            keysRow5.Children.Add(enterKey.RenderKey());
        }

        private Key GetKeyAndAddToList(string text)
        {
            Key k = new Key(font,text,margin);
            k.keyPressedHandler += keyPressedHandler;
            keys.Add(k);
            return k;
        }

        void keyPressedHandler(object sender, KeyPressedEventArgs args)
        {
            if (args.KeyPressed == "Delete")
            {
                if (displayText.TextContent.Length == 0) return;
                displayText.TextContent = displayText.TextContent.Substring(0,displayText.TextContent.Length-1);
                OnTextChanged(sender);
            }
            else if (args.KeyPressed == "Enter")
            {
                OnTextChanged(sender);
                OnEnterPressed(sender);
            }
            else
            {
                displayText.TextContent = displayText.TextContent + args.KeyPressed;
                OnTextChanged(sender);
            }
        }

        private void OnEnterPressed(object sender)
        {
            if (EnterPressed != null)
            {
                EnterPressed(sender,new EnterPressedEventArgs(displayText.TextContent));
            }
        }

        private void OnTextChanged(object sender)
        {
            if (TextChanged != null)
            {
                TextChanged(sender, new TextChangedEventArgs(displayText.TextContent));
            }
        }

        private void UnShiftKeys(bool needRemoval = true)
        {
            if (needRemoval)
            {
                RemoveKeys();
            }

            AddKeys(keysRow1, "789");
            AddKeys(keysRow2, "456");
            AddKeys(keysRow3, "123");
            AddKeys(keysRow4, "0.");
        }

        private void AddKeys(StackPanel keysRow, string keys)
        {
            foreach (var character in keys)
            {
                keysRow.Children.Add(GetKeyAndAddToList(character.ToString()).RenderKey());
            }
        }

        private void RemoveKeys()
        {
            foreach (Key k in keys)
            {
                k.keyPressedHandler -= keyPressedHandler;
                k.Dispose();
            }
            keysRow1.Children.Clear();
            keysRow2.Children.Clear();
            keysRow3.Children.Clear();
            keysRow4.Children.Clear();
        }
    }
}