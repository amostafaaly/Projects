using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

	public class Course : BaseEntity
	{
		public Course()
		{
			Name = Id ?? "Course";
		}

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
	
	   public string GetDetails() => $"{Id} {Description}";
    }