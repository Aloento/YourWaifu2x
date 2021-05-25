namespace YourWaifu2x.Entities.Data {
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Windows.Storage;
    using Helpers;

    public static class WaifuInstance {
        public static readonly Thread WaifuThread = new Thread(() => Waifu2X = Waifu2X ?? new Waifu2X()) {
            Name = "Waifu2x-Vulkan-Library",
            IsBackground = true
        };

        public static Waifu2X Waifu2X;
    }

    public static class ImageList {
        public static readonly ObservableCollection<IStorageItem2> ImageListData =
            new ObservableCollection<IStorageItem2>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WaifuConfig {
        public string Format;
        public IntVector Gpu;
        public string Input;
        public IntVector JobProc;
        public int JobSava;
        public int JobsLoad;
        public string Model;
        public int Noise;
        public string Output;
        public int Scale;
        public IntVector TileSize;
        public int TtaMode;
    }
}
