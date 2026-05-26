using Projects.Src.DTOs.CourseDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Shared;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.UI
{
    public class CourseMenu
    {
        private readonly ICourseService _service;

        public CourseMenu(ICourseService service)
        {
            _service = service;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("Course Management");
                Console.WriteLine("  1. Add New Course");
                Console.WriteLine("  2. View All Courses");
                Console.WriteLine("  3. Search Course by ID");
                Console.WriteLine("  4. Search Courses by Faculty");
                Console.WriteLine("  5. Assign Instructor to Course");
                Console.WriteLine("  6. Enroll Student in Course");
                Console.WriteLine("  7. Update Student Raw Score");
                Console.WriteLine("  8. View Student Enrollments");
                Console.WriteLine("  9. Update Course");
                Console.WriteLine("  10. Delete Course");
                Console.WriteLine(" -1. Back to Main Menu\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            UIHelper.DrawHeader("Add New Course");
                            var createDto = new CreateCourseDto
                            {
                                Name = InputValidator.GetValidString("Course Name: "),
                                CreditHours = InputValidator.GetValidInt("Credit Hours: "),
                                Description = InputValidator.GetValidString("Description: "),
                                Faculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ")
                            };
                            _service.CreateCourse(createDto);
                            UIHelper.ShowSuccess("Course added successfully.");
                            break;

                        case 2:
                            UIHelper.DrawHeader("All Courses");
                            var courses = _service.GetAllCourses();
                            if (!courses.Any())
                                UIHelper.ShowWarning("No courses found.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var course in courses)
                                    Console.WriteLine($"  {course.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 3:
                            UIHelper.DrawHeader("Search Course by ID");
                            string searchId = InputValidator.GetValidString("Enter Course ID: ");
                            var foundCourse = _service.GetCourseById(searchId);
                            if (foundCourse == null)
                                throw new EntityNotFoundException("Course", searchId);
                            Console.WriteLine($"\n  {foundCourse.GetDetails()}");
                            break;

                        case 4:
                            UIHelper.DrawHeader("Search Courses by Faculty");
                            Faculty searchFaculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ");
                            var facultyCourses = _service.GetCoursesByFaculty(searchFaculty);
                            if (!facultyCourses.Any())
                                UIHelper.ShowWarning($"No courses found for {searchFaculty}.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var course in facultyCourses)
                                    Console.WriteLine($"  {course.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 5:
                            UIHelper.DrawHeader("Assign Instructor to Course");
                            string instructorId = InputValidator.GetValidString("Instructor ID: ");
                            string courseId = InputValidator.GetValidString("Course ID: ");
                            _service.AssignInstructor(courseId, instructorId);
                            UIHelper.ShowSuccess("Instructor assigned to course successfully.");
                            break;

                        case 6:
                            UIHelper.DrawHeader("Enroll Student in Course");
                            string studentId = InputValidator.GetValidString("Student ID: ");
                            string enrollCourseId = InputValidator.GetValidString("Course ID: ");
                            _service.AssignStudent(studentId, enrollCourseId);
                            UIHelper.ShowSuccess("Student enrolled in course successfully.");
                            break;

                        case 7:
                            UIHelper.DrawHeader("Update Student Raw Score");
                            string rawStudentId = InputValidator.GetValidString("Student ID: ");
                            string rawCourseId = InputValidator.GetValidString("Course ID: ");
                            double score = Convert.ToDouble(InputValidator.GetValidDecimal("New Raw Score: "));
                            _service.UpdateCourseRawScore(rawStudentId, rawCourseId, score);
                            UIHelper.ShowSuccess("Student raw score updated successfully.");
                            break;

                        case 8:
                            UIHelper.DrawHeader("View Student Enrollments");
                            string enrollmentStudentId = InputValidator.GetValidString("Student ID: ");
                            var enrollments = _service.GetCoursesForStudent(enrollmentStudentId);
                            if (!enrollments.Any())
                                UIHelper.ShowWarning("No enrollments found for that student.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var enrollment in enrollments)
                                    Console.WriteLine($"  [Enrollment] {enrollment.CourseId} — {enrollment.CourseName} | Score: {(enrollment.RawScore.HasValue ? enrollment.RawScore.Value.ToString() : "N/A")}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 9:
                            UIHelper.DrawHeader("Update Course");
                            string updateId = InputValidator.GetValidString("Course ID to Update: ");
                            var existingCourse = _service.GetCourseById(updateId);
                            if (existingCourse == null)
                                throw new EntityNotFoundException("Course", updateId);
                            var updateDto = new UpdateCourseDto
                            {
                                Id = updateId,
                                Name = InputValidator.GetValidString("New Course Name: "),
                                CreditHours = InputValidator.GetValidInt("New Credit Hours: "),
                                Description = InputValidator.GetValidString("New Description: "),
                                Faculty = (Faculty)InputValidator.GetValidInt("New Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ")
                            };
                            _service.UpdateCourse(updateDto);
                            UIHelper.ShowSuccess("Course updated successfully.");
                            break;

                        case 10:
                            UIHelper.DrawHeader("Delete Course");
                            string deleteId = InputValidator.GetValidString("Enter Course ID to delete: ");
                            _service.DeleteCourse(deleteId);
                            UIHelper.ShowSuccess("Course deleted successfully.");
                            break;
                    }
                }
                catch (SchoolException ex)
                {
                    UIHelper.ShowError(ex.Message);
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError($"An unexpected error occurred: {ex.Message}");
                }

                if (choice != -1)
                    UIHelper.PressAnyKeyToContinue();

            } while (choice != -1);
        }
    }
}
