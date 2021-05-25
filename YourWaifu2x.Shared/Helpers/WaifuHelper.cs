namespace YourWaifu2x.Helpers {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Data;

    public class Waifu2X : Waifu2xWrapper {
        private readonly Queue<Task<int>> queue = new Queue<Task<int>>();
        private static bool locker;

        public Task<int> Submit(WaifuConfig config) {
            var task = new Task<int>(() => {
                if (config.Input != null) {
                    setInput(config.Input.Path);
                }
                if (config.Output != null) {
                    setOutput(config.Output.Path);
                }
                if (config.Noise != 0) {
                    setNoise(config.Noise);
                }
                if (config.Scale != 0) {
                    setScale(config.Scale);
                }
                if (config.TileSize != null) {
                    setTileSize(config.TileSize);
                }
                if (config.Model != null) {
                    setModel(config.Model.Path);
                }
                if (config.Gpu != null) {
                    setGpu(config.Gpu);
                }
                if (config.JobsLoad != 0) {
                    setJobsLoad(config.JobsLoad);
                }
                if (config.JobProc != null) {
                    setJobProc(config.JobProc);
                }
                if (config.JobSave != 0) {
                    setJobSave(config.JobSave);
                }
                if (config.TtaMode != 0) {
                    setTtaMode(config.TtaMode);
                }
                if (config.Format != null) {
                    setFormat(config.Format);
                }
                return execute();
            });
            queue.Enqueue(task);
            return task;
        }

        public void Start() {
            if (locker)
                return;

            Task.Run(() => {
                locker = true;
                while (queue.Count != 0) {
                    var task = queue.Dequeue();
                    task.Start();
                    task.Wait();
                }
                locker = false;
            }).Start();
        }

        private void Process() {
            while (queue.Count != 0) {

            }
        }
    }
}
