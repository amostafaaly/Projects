using Projects.Src.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models.Courses
{
	public class Course : BaseEntity
	{
		public string Description { get; set; } = string.Empty;
		private int _creditHours;
		public int CreditHours
		{
			get => _creditHours;
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException(nameof(CreditHours), "Credit hours must be greater than zero.");
				_creditHours = value;
			}
		}
		public string? InstructorId { get; set; }
		public List<StudentCourse> Enrollments { get; set; } = new List<StudentCourse>();
		public Faculty Faculty { get; set; }
		public string ToFileLine()
		{
			return $"{Id},{Description},{CreditHours},{InstructorId},{Faculty}";
        }	
		public void FromFileLine(string line)
		{
			var parts = line.Split(',');
			if (parts.Length != 5)
				throw new FormatException("Invalid line format for Course.");
			Id = parts[0];
			Description = parts[1];
			CreditHours = int.Parse(parts[2]);
			InstructorId = parts[3];
			Faculty = Enum.Parse<Faculty>(parts[4]);
		}
    }
}
