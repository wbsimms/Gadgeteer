using System;
using Microsoft.SPOT;

namespace TouchKeyboard
{
    public class KeyPressedEventArgs : EventArgs
    {
        private string keyPressed = "";

        public KeyPressedEventArgs(string key)
        {
            this.keyPressed = key;
        }
        public string KeyPressed { get { return this.keyPressed; } }
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

    public class EnterPressedEventArgs : EventArgs
    {
        private string text;
        public string Text { get { return text; } }

        public EnterPressedEventArgs(string text)
        {
            this.text = text;
        }
    }
}
