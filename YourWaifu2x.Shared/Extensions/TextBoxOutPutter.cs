namespace YourWaifu2x.Extensions {
    using System;
    using System.IO;
    using System.Text;
    using Windows.UI.Xaml.Controls;

    public sealed class TextBoxOutPutter : TextWriter {
        private readonly TextBox textBox;

        public TextBoxOutPutter(TextBox output) => textBox = output ?? throw new NullReferenceException();

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value) {
            base.Write(value);
            _ = textBox.Dispatcher.RunIdleAsync(_ =>
                  textBox.Text += value.ToString());
        }
    }
}
