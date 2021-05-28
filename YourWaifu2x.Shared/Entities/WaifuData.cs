namespace YourWaifu2x.Entities.Data {
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
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

        public static void Init() =>
            Task.Run(() => {
                _ = WaitingList.ToString();
                _ = FinishedList.ToString();
                _ = ErrorList.ToString();
                _ = Waifu2X.ToString();
            });
    }

    public class WaifuConfig {
        public string Format;
        public IntVector Gpu;
        public IStorageItem2 Input;
        public IntVector JobProc;
        public int JobSave;
        public int JobsLoad;
        public string Model;
        public int Noise;
        public IStorageItem2 Output;
        public int Scale;
        public IntVector TileSize;
        public int TtaMode;
        public bool Result;
    }
}
