using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Services.FileHandler
{
    public static class FileHandler
    {
        public static void SaveToFile(List<string> items, string fileName)
        {
            try
            {
                if (!Directory.Exists(AppPaths.Data))
                    Directory.CreateDirectory(AppPaths.Data);

                var path = Path.Combine(AppPaths.Data, fileName);

                File.WriteAllLines(path, items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        public static List<string> LoadAll(string fileName)
        {
            try
            {
                var path = Path.Combine(AppPaths.Data, fileName);

                if (!File.Exists(path))
                {
                    Console.WriteLine("File not found. Returning empty list.");
                    return new List<string>();
                }

                return new List<string>(File.ReadAllLines(path));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                return new List<string>();
            }
        }
        public static class AppPaths
        {
            public static string Root =>
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

            public static string Data =>
                Path.Combine(Root, "data");
        }
    }
}
