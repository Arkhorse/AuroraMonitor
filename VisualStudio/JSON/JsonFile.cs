using AuroraMonitor.Utilities.Logger;
using System.Text.Json;

namespace AuroraMonitor.JSON
{
    public class JsonFile
    {
        public static JsonSerializerOptions DefaultOptions { get; } = new JsonSerializerOptions()
        {
            WriteIndented = true, // pretty print
            IncludeFields = true // use [JsonInclude] on properties you want to include, otherwise it wont be
        };

        #region Syncronous
        public static void Save<T>(string configFileName, T? Tinput, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                using FileStream file = File.Open(configFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                JsonSerializer.Serialize<T?>(file, Tinput, options);
                file.Dispose();
            }
            catch (Exception e)
            {
                Main.Logger.Log(FlaggedLoggingLevel.Critical, $"Attempting to save {configFileName} failed", e);
            }
        }

        public static T? Load<T>(string configFileName, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                using FileStream file = File.Open(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                T? output = JsonSerializer.Deserialize<T?>(file, options);
                file.Dispose();
                return output;
            }
            catch
            {
                Main.Logger.Log(FlaggedLoggingLevel.Critical, $"Attempting to load {configFileName} failed");
                throw;
            }
        }
        #endregion
        #region Async
        /// <summary>
        /// Loads a given JSON file
        /// </summary>
        /// <typeparam name="T">The class to deserialize</typeparam>
        /// <param name="configFileName">absolute path to the file</param>
        /// <returns>new class based on file contents</returns>
        public static async Task<T?> LoadAsync<T>(string configFileName, JsonSerializerOptions? options = null)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming
            //Task taskA = Task.Run( () => Console.WriteLine("Hello from taskA."));
            //https://stackoverflow.com/questions/38423472/what-is-the-difference-between-task-run-and-task-factory-startnew
            /*
                in the .NET Framework 4.5 Developer Preview, we’ve introduced the new Task.Run method. This in no way obsoletes Task.Factory.StartNew,
                but rather should simply be thought of as a quick way to use Task.Factory.StartNew without needing to specify a bunch of parameters.
                It’s a shortcut. In fact, Task.Run is actually implemented in terms of the same logic used for Task.Factory.StartNew, just passing in
                some default parameters. When you pass an Action to Task.Run:

                'Task.Run(someAction);'

                it's exactly equivalent to:

                'Task.Factory.StartNew(someAction, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);'
             */

            return await Task.Run(() => Load<T>(configFileName, options));
        }

        /// <summary>
        /// Saves a new JSON file
        /// </summary>
        /// <typeparam name="T">The class to serialize</typeparam>
        /// <param name="configFileName">absolute path to the file</param>
        /// <param name="Tinput">an instance of the given class with information filled</param>
        public static async Task SaveAsync<T>(string configFileName, T? Tinput, JsonSerializerOptions? options = null)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming
            //Task taskA = Task.Run( () => Console.WriteLine("Hello from taskA."));
            //https://stackoverflow.com/questions/38423472/what-is-the-difference-between-task-run-and-task-factory-startnew
            /*
                in the .NET Framework 4.5 Developer Preview, we’ve introduced the new Task.Run method. This in no way obsoletes Task.Factory.StartNew,
                but rather should simply be thought of as a quick way to use Task.Factory.StartNew without needing to specify a bunch of parameters.
                It’s a shortcut. In fact, Task.Run is actually implemented in terms of the same logic used for Task.Factory.StartNew, just passing in
                some default parameters. When you pass an Action to Task.Run:

                'Task.Run(someAction);'

                it's exactly equivalent to:

                'Task.Factory.StartNew(someAction, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);'
             */

            await Task.Run(() => Save<T>(configFileName, Tinput, options));
        }
        #endregion
    }
}
