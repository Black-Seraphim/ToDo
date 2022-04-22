using System;
using System.IO;
using System.Text.Json;
using ToDo.Logging;
using ToDo.Model;

namespace ToDo.Database
{
    /// <summary>
    /// Provides methods to load a Task-Model from a json-based file. Save a model into that file. And create a new empty file.
    /// </summary>
    public static class Json
    {
        private static readonly Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        private static readonly string path = Path.Combine(Environment.GetFolderPath(folder), "BlackSeraphim", "ToDo");
        private static readonly string databaseName = Path.Join(path, "database.json");

        /// <summary>
        /// Reads the json-database an deserialize it to an TaskList Model.
        /// </summary>
        /// <returns>A TaskList-BaseModel that was deserialized from the json-Database</returns>
        public static BaseModel Load()
        {
            try
            {
                string json = File.ReadAllText(databaseName);
                BaseModel model = JsonSerializer.Deserialize<BaseModel>(json);
                return model;
            }
            catch (Exception ex)
            {
                Log.WriteEntry($"JSON-Database could not be loaded. Reason: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Serialize a TaskList Model and store it into a json-file
        /// </summary>
        /// <param name="model">The Model that should be stored to the json-file</param>
        public static void Save(BaseModel model)
        {
            _ = Directory.CreateDirectory(path);
            JsonSerializerOptions serializeOptions = new() { WriteIndented = true };
            string json = JsonSerializer.Serialize(model, serializeOptions);
            File.WriteAllText(databaseName, json);
            Log.WriteEntry($"JSON-Database saved.");
        }

        /// <summary>
        /// Create a new json-database for TaskList Model
        /// </summary>
        public static void CreateNew()
        {
            Directory.CreateDirectory(path);
            string jsonFrame = JsonSerializer.Serialize(new BaseModel());
            File.WriteAllText(databaseName, jsonFrame);
            Log.WriteEntry($"New JSON-Database successfully created.");
        }
    }
}
