namespace YourWaifu2x.Helpers {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities.Data;

    public sealed class Waifu2X : Waifu2xWrapper {
        private readonly Queue<Task<WaifuConfig>> queue = new Queue<Task<WaifuConfig>>();
        private static bool locker;

        public Task<WaifuConfig> Submit(WaifuConfig config) {
            var task = new Task<WaifuConfig>(() => {
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
                    setModel(config.Model);
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

                config.Result = execute() == 0;
                return config;
            });
            queue.Enqueue(task);
            return task;
        }

        public void Start() {
            if (locker)
                return;

            _ = Task.Run(() => {
                locker = true;
                while (queue.Count != 0) {
                    var task = queue.Dequeue();
                    task.Start();
                    task.Wait();
                    if (task.Result.Result) {
                        _ = WaifuInstance.WaitingList.Remove(task.Result.Input);
                        WaifuInstance.FinishedList.Add(task.Result.Output);
                    } else {
                        WaifuInstance.ErrorList.Add(task.Result.Input);
                    }
                }
                locker = false;
            });
        }
    }
}
