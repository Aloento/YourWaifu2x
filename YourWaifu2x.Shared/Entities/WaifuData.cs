namespace YourWaifu2x.Entities.Data {
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Helpers;

    internal static class WaifuInstance {
        internal static readonly ObservableCollection<IStorageItem2> WaitingList =
            new ObservableCollection<IStorageItem2>();

        internal static readonly ObservableCollection<IStorageItem2> FinishedList =
            new ObservableCollection<IStorageItem2>();

        internal static readonly ObservableCollection<IStorageItem2> ErrorList =
            new ObservableCollection<IStorageItem2>();

        internal static readonly WaifuConfig Config = new WaifuConfig();

        internal static readonly Waifu2X Waifu2X = new Waifu2X();

        internal static void Init() =>
            Task.Run(() => {
                _ = WaitingList.ToString();
                _ = FinishedList.ToString();
                _ = ErrorList.ToString();
                _ = Waifu2X.ToString();
            });
    }

    internal sealed class WaifuConfig {
        internal string Format;
        internal IntVector Gpu;
        internal IStorageItem2 Input;
        internal IntVector JobProc;
        internal int JobSave;
        internal int JobsLoad;
        internal string Model;
        internal int Noise;
        internal IStorageItem2 Output;
        internal int Scale;
        internal IntVector TileSize;
        internal int TtaMode;
        internal bool Result;
    }
}
