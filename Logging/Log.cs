using System;
using System.IO;

namespace ToDo.Logging
{
    /// <summary>
    /// Provides a method to log Messages into a txt-logfile.
    /// </summary>
    public static class Log
    {
        private static readonly Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        private static readonly string path = Path.Combine(Environment.GetFolderPath(folder), "BlackSeraphim", "ToDo");
        private static readonly string fileName = Path.Join(path, "logfile.txt");

        /// <summary>
        /// Create a new log-entry with date and time in a predefined logfile.
        /// </summary>
        /// <param name="logMessage">Message that should be logged</param>
        public static void WriteEntry(string logMessage)
        {
            _ = Directory.CreateDirectory(path);
            using StreamWriter w = File.AppendText(fileName);
            w.WriteLine($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} - {logMessage}");
        }
    }
}
