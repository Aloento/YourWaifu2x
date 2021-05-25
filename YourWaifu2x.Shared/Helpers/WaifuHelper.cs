namespace YourWaifu2x.Helpers {
    using System.Threading.Tasks;
    using Entities.Data;

    public class Waifu2X : Waifu2xWrapper {
        private static bool locker = false;

        public async Task<bool> StartAsync(WaifuConfig config) {
            if (locker)
                return false;

            return true;
        }
    }
}
