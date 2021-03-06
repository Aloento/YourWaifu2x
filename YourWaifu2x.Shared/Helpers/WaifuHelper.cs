namespace YourWaifu2x.Helpers {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Entities.Data;
    using Views.MyPages;

    internal sealed class Waifu2X : Waifu2xWrapper {
        private readonly Queue<Task<WaifuConfig>> queue = new Queue<Task<WaifuConfig>>();
        private static bool locker;

        internal void Submit(WaifuConfig config) =>
            queue.Enqueue(new Task<WaifuConfig>(() => {
                if (config.Input == null || config.Output == null) {
                    config.Result = false;
                    return config;
                }

                setInput(config.Input.Path);
                setOutput(config.Output.Path + Path.DirectorySeparatorChar + config.Input.Name);

                if (config.Noise != 0)
                    setNoise(config.Noise);

                if (config.Scale != 0)
                    setScale(config.Scale);

                if (config.TileSize != null)
                    setTileSize(config.TileSize);

                if (config.Model != null)
                    setModel(config.Model);

                if (config.Gpu != null)
                    setGpu(config.Gpu);

                if (config.JobsLoad != 0)
                    setJobsLoad(config.JobsLoad);

                if (config.JobProc != null)
                    setJobProc(config.JobProc);

                if (config.JobSave != 0)
                    setJobSave(config.JobSave);

                if (config.TtaMode != 0)
                    setTtaMode(config.TtaMode);

                if (config.Format != null)
                    setFormat(config.Format);

                config.Result = execute() == 0;
                return config;
            }));

        internal void Start(Exporting exporting) {
            if (locker)
                return;

            _ = Task.Run(() => {
                locker = true;
                while (queue.Count != 0) {
                    var task = queue.Dequeue();
                    task.Start();
                    task.Wait();

                    _ = exporting.Dispatcher.RunIdleAsync(_ => {
                        WaifuInstance.WaitingList.Remove(task.Result.Input);
                        if (task.Result.Result) {
                            WaifuInstance.FinishedList.Add(task.Result.Input);
                            Console.Out.WriteLineAsync("Success: " + task.Result.Output.Path +
                                                       Path.DirectorySeparatorChar + task.Result.Input.Name);
                        } else {
                            WaifuInstance.ErrorList.Add(task.Result.Input);
                            Console.Error.WriteLineAsync("Error: " + task.Result.Input.Path);
                            Console.Error.WriteLineAsync(task.Result.ToString());
                        }
                    });
                }
                locker = false;
            });
        }
    }
}
