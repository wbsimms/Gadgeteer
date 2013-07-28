using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using TouchKeyboard;

namespace GadgeteerHelper
{
    public class Key : IDisposable
    {
        private string text = "";
        private int margin = 9;
        private Font font;
        public delegate void KeyPressedEventHander(object sender, KeyPressedEventArgs args);
        public event KeyPressedEventHander keyPressedHandler;
        private Border b;
        private Text t;

        public Key(Font font, string text, int margin = 9)
        {
            this.text = text;
            this.font = font;
            this.margin = margin;
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
            Border border = sender as Border;
            if (border == null)
            {
                return;
            }

            Text t = border.Child as Text;

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
}