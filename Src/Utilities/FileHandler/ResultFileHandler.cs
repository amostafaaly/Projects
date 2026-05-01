using Projects.Src.Models;
using Projects.Src.Contracts;
using System;

namespace Projects.Src.Utilities.FileHandler
{
    public class ResultFileHandler : IFileHandler<Result>
    {
        public string ToFileLine(Result item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return $"{item.Id},{item.StudentId},{item.ExamId},{item.Score},{item.TotalMarks}";
        }

        public Result FromFileLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) throw new ArgumentException("line is empty", nameof(line));
            var parts = line.Split(',');
            if (parts.Length != 5)
                throw new FormatException("Invalid line format for Result.");

            var result = new Result();
            result.Id = parts[0];
            result.StudentId = parts[1];
            result.ExamId = parts[2];
            result.Score = int.Parse(parts[3]);
            result.TotalMarks = int.Parse(parts[4]);
            return result;
        }
    }
}
