using Projects.Src.Managers;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.UI_layer
{
    public class StudentMenu
    {
        private readonly StudentManager _studentManager;

        public StudentMenu(StudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Student Menu ---");
                Console.WriteLine("1- Register Student");
                Console.WriteLine("2- Promote to Graduate");
                Console.WriteLine("3- Change Faculty");
                Console.WriteLine("4- Change Status");
                Console.WriteLine("5- Delete Student");
                Console.WriteLine("6- Get Student");
                Console.WriteLine("7- Get All");
                Console.WriteLine("-1 Exit");

                int.TryParse(Console.ReadLine(), out int n);
                if (n == -1) break;

                switch (n)
                {
                    case 1:
                        Console.Write("First Name: ");
                        string f = Console.ReadLine();

                        Console.Write("Last Name: ");
                        string l = Console.ReadLine();

                        Console.Write("Year: ");
                        int y = int.Parse(Console.ReadLine());

                        Console.Write("Faculty: ");
                        Faculty faculty = (Faculty)int.Parse(Console.ReadLine());

                        var s = _studentManager.RegisterStudent(f, l, y, faculty);
                        Console.WriteLine(s.GetDetails());
                        break;

                    case 2:
                        Console.WriteLine("enter the student id");
                        _studentManager.PromoteToGraduate(Console.ReadLine());
                        break;

                    case 3:
                        Console.Write("ID: ");
                        string id = Console.ReadLine();

                        Console.Write("Faculty (0 = Cs, 1 = Engineering, 2 = Arts, 3 = Business, 4 = Science): ");
                        Faculty newF = (Faculty)int.Parse(Console.ReadLine());

                        _studentManager.ChangeFaculty(id, newF);
                        break;

                    case 4:
                        Console.Write("ID: ");
                        string sid = Console.ReadLine();

                        Console.Write("Status: 0-active 1-graduated 3-Withdraw");
                        StudentStatus st = (StudentStatus)int.Parse(Console.ReadLine());

                        _studentManager.ChangeStatus(sid, st);
                        break;

                    case 5:
                        Console.WriteLine("enter the id");
                        _studentManager.DeleteStudent(Console.ReadLine());
                        break;

                    case 6:
                        var student = _studentManager.GetStudent(Console.ReadLine());
                        Console.WriteLine(student.GetDetails());
                        break;

                    case 7:
                        foreach (var stt in _studentManager.AllStudents)
                            Console.WriteLine(stt.GetDetails());
                        break;
                }
            }
        }
    }
}
