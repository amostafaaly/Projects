using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projects.Src.Utilities.FileHandler
{
    public static class FileHandler
    {
        // SaveAll: writes each string in 'items' as a single line to the given file (relative to AppPaths.Data)
        public static void SaveAll(List<string> items, string fileName)
        {
            try
            {
                if (!Directory.Exists(AppPaths.Data))
                    Directory.CreateDirectory(AppPaths.Data);

                var path = Path.Combine(AppPaths.Data, fileName);

                using (var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    foreach (var line in items)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        // LoadAll: reads all lines from the file and returns as list of strings.
        // If the file is missing, returns an empty list.
        public static List<string> LoadAll(string fileName)
        {
            try
            {
                var path = Path.Combine(AppPaths.Data, fileName);

                if (!File.Exists(path))
                {
                    // Missing file on first run is expected; return empty list
                    return new List<string>();
                }

                var result = new List<string>();
                using (var reader = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                return new List<string>();
            }
        }

        // Backwards compatibility wrapper
        public static void SaveToFile(List<string> items, string fileName) => SaveAll(items, fileName);
        public static class AppPaths
        {
            public static string Root =>
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

            public static string Data =>
                Path.Combine(Root, "data");
        }
    }
}
