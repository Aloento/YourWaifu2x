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
        public IStorageItem2 Input;
        public IntVector JobProc;
        public int JobSave;
        public int JobsLoad;
        public IStorageItem2 Model;
        public int Noise;
        public IStorageItem2 Output;
        public int Scale;
        public IntVector TileSize;
        public int TtaMode;
    }
}
