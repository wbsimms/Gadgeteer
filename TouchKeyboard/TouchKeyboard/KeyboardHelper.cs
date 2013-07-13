using System;
using System.Collections;
using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;
using Color = Microsoft.SPOT.Presentation.Media.Color;

namespace GadgeteerHelper
{

    public class KeyboardHelper
    {
        private Display_T35 display;
        private Font font;
        private Text displayText;
        private bool keysShifted = false;
        private bool numPad = false;
        private IList keys = new ArrayList();
        public delegate void TextChangedEventHander(object sender, TextChangedEventArgs args);
        public event TextChangedEventHander TextChanged;


        StackPanel spacer = new StackPanel(Orientation.Horizontal);
        StackPanel spacer1 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow1 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow2 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow3 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow4 = new StackPanel(Orientation.Horizontal);
        StackPanel keysRow5 = new StackPanel(Orientation.Horizontal);
 
        public KeyboardHelper(Display_T35 display, Font font)
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

            Key shiftKey = new Key(font, "Shift");
            shiftKey.keyPressedHandler += keyPressedHandler;
            Key spaceKey = new Key(font, "Space");
            spaceKey.keyPressedHandler += keyPressedHandler;
            Key delKey = new Key(font, "Delete");
            delKey.keyPressedHandler += keyPressedHandler;
            keysRow5.Children.Add(shiftKey.RenderKey());
            keysRow5.Children.Add(spaceKey.RenderKey());
            keysRow5.Children.Add(delKey.RenderKey());

        }

        private Key GetKeyAndAddToList(string text)
        {
            Key k = new Key(font,text);
            k.keyPressedHandler += keyPressedHandler;
            keys.Add(k);
            return k;
        }

        void keyPressedHandler(object sender, KeyPressedEventArgs args)
        {
            
            if (args.KeyPressed == "Shift" && !keysShifted)
            {
                keysShifted = true;
                ShiftKeys();
            }
            else if (args.KeyPressed == "Shift" && keysShifted)
            {
                keysShifted = false;
                UnShiftKeys(true);
            }
            else if (args.KeyPressed == "Space")
            {
                displayText.TextContent = displayText.TextContent + " ";
            }
            else if (args.KeyPressed == "Delete")
            {
                if (displayText.TextContent.Length == 0) return;
                displayText.TextContent = displayText.TextContent.Substring(0,displayText.TextContent.Length-1);
            }
            else
            {
                displayText.TextContent = displayText.TextContent + args.KeyPressed;
                if (TextChanged != null)
                {
                    TextChanged(sender,new TextChangedEventArgs(displayText.TextContent));
                }
            }
        }

        private void UnShiftKeys(bool needRemoval)
        {
            if (needRemoval)
            {
                RemoveKeys();
            }

            keysRow1.Children.Add(GetKeyAndAddToList("1").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("2").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("3").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("4").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("5").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("6").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("7").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("8").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("9").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("0").RenderKey());

            keysRow2.Children.Add(GetKeyAndAddToList("q").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("w").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("e").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("r").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("t").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("y").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("u").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("i").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("o").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("p").RenderKey());

            keysRow3.Children.Add(GetKeyAndAddToList("a").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("s").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("d").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("f").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("g").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("h").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("j").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("k").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("l").RenderKey());

            keysRow4.Children.Add(GetKeyAndAddToList("z").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("x").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("c").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("v").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("b").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("n").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("m").RenderKey());


        }

        private void ShiftKeys()
        {
            RemoveKeys();
            keysRow1.Children.Add(GetKeyAndAddToList("!").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("@").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("#").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("$").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("%").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("^").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("&").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("*").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList("(").RenderKey());
            keysRow1.Children.Add(GetKeyAndAddToList(")").RenderKey());

            keysRow2.Children.Add(GetKeyAndAddToList("Q").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("W").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("E").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("R").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("T").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("Y").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("U").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("I").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("O").RenderKey());
            keysRow2.Children.Add(GetKeyAndAddToList("P").RenderKey());

            keysRow3.Children.Add(GetKeyAndAddToList("A").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("S").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("D").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("F").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("G").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("H").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("J").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("K").RenderKey());
            keysRow3.Children.Add(GetKeyAndAddToList("L").RenderKey());

            keysRow4.Children.Add(GetKeyAndAddToList("Z").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("X").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("C").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("V").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("B").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("N").RenderKey());
            keysRow4.Children.Add(GetKeyAndAddToList("M").RenderKey());
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


    public class Key : IDisposable
    {
        private string text = "";
        private int width = 10;
        private int height = 10;
        private int padding = 10;
        private int margin = 9;
        private Font font;
        public delegate void KeyPressedEventHander(object sender, KeyPressedEventArgs args);
        public event KeyPressedEventHander keyPressedHandler;
        private Border b;
        private Text t;

        public Key(Font font, string text)
        {
            this.text = text;
            this.font = font;
        }

        public Border RenderKey()
        {
            b = new Border();
            t = new Text(font,text);
            t.SetMargin(margin,margin,margin,margin);
            b.SetMargin(3);
            b.Foreground = new SolidColorBrush(Colors.White);
            b.BorderBrush = new SolidColorBrush(Colors.Black);
            t.ForeColor = Colors.White;
            b.Child = t;
            t.TouchUp += b_TouchUp;
            b.TouchUp += b_TouchUp;
            return b;
        }

        void b_TouchUp(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            Text t = ((Border) sender).Child as Text;
            string content = t.TextContent;
            if (keyPressedHandler != null)
            {
                KeyPressedEventArgs args = new KeyPressedEventArgs(content);
                keyPressedHandler(sender, args);
            }
        }

        public void Dispose()
        {
            b.TouchUp -= b_TouchUp;
            t.TouchUp -= b_TouchUp;
        }
    }

    public class KeyPressedEventArgs : EventArgs
    {
        private string keyPressed = "";

        public KeyPressedEventArgs(string key)
        {
            this.keyPressed = key;
        }
        public string KeyPressed { get { return this.keyPressed; }}
    }

    public class TextChangedEventArgs : EventArgs
    {
        private string text;
        public string Text { get { return text; } }

        public TextChangedEventArgs(string text)
        {
            this.text = text;
        }
    }


}
