namespace YourWaifu2x {
    using System.ComponentModel;

    public enum SourceSdk {
        [Description("WinUI/Uno.UI")]
        WinUI,
        [Description("Uno.Material")]
        UnoMaterial,
        [Description("Windows Community Toolkit")]
        WCT,
        [Description("Uno.Toolkit")]
        UnoToolkit
    }
}
