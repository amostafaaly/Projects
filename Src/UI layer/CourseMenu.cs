using Projects.Src.Managers;
using Projects.Src.Models;
using Projects.Src.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.UI_layer
{
    public class CourseMenu
    {
        private readonly CourseService _courseService;
        private readonly CourseManager _courseManager;
        private readonly InstructorManager _instructorManager;

        public CourseMenu(
            CourseService courseService,
            CourseManager courseManager,
            InstructorManager instructorManager)
        {
            _courseService = courseService;
            _courseManager = courseManager;
            _instructorManager = instructorManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Course Menu ---");
                Console.WriteLine("1- Create Course");
                Console.WriteLine("2- Assign Instructor");
                Console.WriteLine("3- Get Course by ID");
                Console.WriteLine("4- Display All Courses");
                Console.WriteLine("5- Delete Course");
                Console.WriteLine("-1 Exit");

                int.TryParse(Console.ReadLine(), out int n);
                if (n == -1) break;

                switch (n)
                {
                    case 1:
                        {
                            Console.Write("Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Credit Hours: ");
                            int hours = int.Parse(Console.ReadLine());

                            Console.Write("Description: ");
                            string desc = Console.ReadLine();

                            Console.Write("Faculty (0-4): ");
                            Faculty faculty = (Faculty)int.Parse(Console.ReadLine());

                            var course = _courseService.CreateCourse(faculty, name, hours, desc);
                            Console.WriteLine(course.GetDetails());
                            
                            break;
                        }

                    case 2:
                        {
                            Console.Write("Course ID: ");
                            string courseId = Console.ReadLine();

                            Console.Write("Instructor ID: ");
                            string instructorId = Console.ReadLine();

                            _courseService.AssignInstructor(courseId, instructorId);

                            Console.WriteLine("Instructor assigned successfully.");
                            break;
                        }

                    case 3:
                        {
                            Console.Write("Course ID: ");
                            var course = _courseManager.GetById(Console.ReadLine());

                            Console.WriteLine(course.GetDetails());
                            break;
                        }

                    case 4:
                        {
                            foreach (var c in _courseManager.GetAll())
                                Console.WriteLine(c.GetDetails());
                            break;
                        }

                    case 5:
                        {
                            Console.Write("Course ID: ");
                            var course=_courseManager.GetById(Console.ReadLine());
                            _courseManager.Remove(course);

                            Console.WriteLine("Course deleted.");
                            break;
                        }
                }
            }
        }
    }
}
