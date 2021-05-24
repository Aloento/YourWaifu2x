namespace YourWaifu2x.Extensions {
    using System;
    using System.IO;
    using System.Text;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;

    public class TextBoxOutPutter : TextWriter {
        private readonly TextBox textBox;

        public TextBoxOutPutter(TextBox output) => textBox = output ?? throw new NullReferenceException();

        public override Encoding Encoding => Encoding.UTF8;

        public override async void Write(char value) {
            // ReSharper disable once MethodHasAsyncOverload
            base.Write(value);
            await textBox.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                textBox.Text += value.ToString();
            });
        }
    }
}
