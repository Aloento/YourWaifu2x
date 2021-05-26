namespace YourWaifu2x.Entities.Data {
    using System.Collections.ObjectModel;
    using Windows.Storage;
    using Helpers;

    public static class WaifuInstance {
        public static readonly ObservableCollection<IStorageItem2> WaitingList =
            new ObservableCollection<IStorageItem2>();

        public static readonly ObservableCollection<IStorageItem2> FinishedList =
            new ObservableCollection<IStorageItem2>();

        public static readonly ObservableCollection<IStorageItem2> ErrorList =
            new ObservableCollection<IStorageItem2>();

        public static readonly Waifu2X Waifu2X = new Waifu2X();
    }

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
        public bool Result;
    }
}
