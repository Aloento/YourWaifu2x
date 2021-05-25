namespace YourWaifu2x.Entities.Data {
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Windows.Storage;

    public static class ImageList {
        public static readonly ObservableCollection<IStorageItem2> ImageListData = new ObservableCollection<IStorageItem2>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WaifuConfig {
        public string Input;
        public string Output;
        public int Noise;
        public int Scale;
        public IntVector TileSize;
        public string Model;
        public IntVector Gpu;
        public int JobsLoad;
        public IntVector JobProc;
        public int JobSava;
        public int TtaMode;
        public string Format;
    }
}
