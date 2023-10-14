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
        public static void Save<T>(string configFileName, T Tinput, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                using FileStream file = File.Open(configFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                JsonSerializer.Serialize<T>(file, Tinput, options);
                file.Dispose();
            }
            catch
            {
                Logging.LogError($"Attempting to save {configFileName} failed");
                throw;
            }
        }

        public static T Load<T>(string configFileName, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                using FileStream file = File.Open(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var output = JsonSerializer.Deserialize<T>(file, options);
                file.Dispose();
                return output;
            }
            catch
            {
                Logging.LogError($"Attempting to load {configFileName} failed");
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
        public static async Task<T> LoadAsync<T>(string configFileName, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                await using FileStream file = File.Open(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var output = await JsonSerializer.DeserializeAsync<T>(file, options);
                await file.DisposeAsync();
                return output;
            }
            catch
            {
                Logging.LogError($"Attempting to load the config file failed, file: {configFileName}");
                throw;
            }
        }

        /// <summary>
        /// Saves a new JSON file
        /// </summary>
        /// <typeparam name="T">The class to serialize</typeparam>
        /// <param name="configFileName">absolute path to the file</param>
        /// <param name="Tinput">an instance of the given class with information filled</param>
        public static async Task SaveAsync<T>(string configFileName, T Tinput, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                await using FileStream file = File.Open(configFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync<T>(file, Tinput, options);
                await file.DisposeAsync();
            }
            catch
            {
                Logging.LogError($"Attempting to save failed");
                throw;
            }
        }
        #endregion
    }
}
