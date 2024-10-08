using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharpExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Course> courseList = new List<Course>()
  {
        new Course() { CourseId = "A001", Name = "C#", Teacher = "Bill", Classroom = "L107", Credit = 3 },
        new Course() { CourseId = "A002", Name = "HTML/CSS", Teacher = "Amos", Classroom = "L104", Credit = 2 },
        new Course() { CourseId = "A003", Name = "JavaScript/jQuery", Teacher = "奚江華", Classroom = "L104", Credit = 3 },
        new Course() { CourseId = "A004", Name = "MSSQL", Teacher = "Jimmy", Classroom = "L202", Credit = 3 },
        new Course() { CourseId = "A005", Name = "MVC5/CoreMVC", Teacher = "奚江華", Classroom = "L107", Credit = 6 },
        new Course() { CourseId = "B001", Name = "VueJS", Teacher = "Jimmy", Classroom = "L104", Credit = 2 },
        new Course() { CourseId = "B002", Name = "DevOps", Teacher = "David", Classroom = "L107", Credit = 3 },
        new Course() { CourseId = "B003", Name = "MongoDB", Teacher = "Ben", Classroom = "L202", Credit = 2 },
        new Course() { CourseId = "B004", Name = "Redis", Teacher = "Ben", Classroom = "L202", Credit = 2 },
        new Course() { CourseId = "B005", Name = "Git", Teacher = "Andy", Classroom = "L107", Credit = 1 },
        new Course() { CourseId = "B006", Name = "Git", Teacher = "Jimmy", Classroom = "L107", Credit = 1 }
  };

            List<Student> studentList = new List<Student>()
  {
        new Student() { StudentId = "S0001", Name = "小新", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A004", "B002", "B003", "B004", "B005" } },
        new Student() { StudentId = "S0002", Name = "妮妮", Gender = GenderOption.Female, CourseList = new List<string>() { "A002", "A003", "B001", "B002", "B005" } },
        new Student() { StudentId = "S0003", Name = "風間", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005", "B001", "B002", "B003", "B004", "B005"  } },
        new Student() { StudentId = "S0004", Name = "阿呆", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005" } },
        new Student() { StudentId = "S0005", Name = "正男", Gender = GenderOption.Male, CourseList = new List<string>() { "B001", "B002", "B003", "B004", "B005" } },
        new Student() { StudentId = "S0006", Name = "小丸子", Gender = GenderOption.Female, CourseList = new List<string>() { "A005" } },
        new Student() { StudentId = "S0007", Name = "小玉", Gender = GenderOption.Female, CourseList = new List<string>() { "A005", "B002", "B003", "B004" } },
  };

            #region 第1題
            // 1. 列出所有課程的名稱
            Console.WriteLine("1. 列出所有課程的名稱");
            {
                Console.WriteLine(string.Join(Environment.NewLine, courseList.Select((x) => x.Name)));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第2題
            // 2. 列出所有在"L107"教室上課的課程ID
            Console.WriteLine("2. 列出所有在'L107'教室上課的課程ID");
            {
                Console.WriteLine(string.Join(Environment.NewLine, courseList.Where(x => x.Classroom == "L107").Select(x => x.CourseId)));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第3題
            // 3. 列出所有在'L107'教室上課，並且學分為3的課程ID
            Console.WriteLine("3. 列出所有在'L107'教室上課，並且學分為3的課程ID");
            {
                Console.WriteLine(string.Join(Environment.NewLine, courseList.Where(x => x.Classroom == "L107" && x.Credit >= 3).Select(x => x.CourseId)));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第4題
            // 4. 列出所有老師的名字(名字不能重複出現)
            Console.WriteLine("4. 列出所有老師的名字(名字不能重複出現)");
            {
                Console.WriteLine(string.Join(Environment.NewLine, courseList.GroupBy((x) => x.Teacher).Select((x) => x.Key)));
                //Distinct()
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第5題
            // 5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)
            Console.WriteLine("5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)");
            {
                Console.WriteLine(string.Join(Environment.NewLine, courseList.Where((x) => x.Classroom == "L202").GroupBy((x) => x.Teacher).Select((x) => x.Key)));
                //Distinct()
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第6題
            // 6. 列出所有女性學生的名字
            Console.WriteLine("6. 列出所有女性學生的名字");
            {
                Console.WriteLine(string.Join(Environment.NewLine, studentList.Where((x) => x.Gender == GenderOption.Female).Select((x) => x.Name)));

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第7題
            // 7. 列出有上'Git'這門課的學員名字
            Console.WriteLine("7. 列出有上'Git'這門課的學員名字");
            {
                Console.WriteLine(string.Join(Environment.NewLine, studentList.Where((x) => x.CourseList.Contains("B005") || x.CourseList.Contains("B006")).Select((x) => x.Name)));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第8題
            // 8. 列出每個學員上的每一堂課
            // ex:
            /*
                       小玉: 
                            MVC5/CoreMVC
                            DevOps
                            MongoDB
                            Redis
                    */
            Console.WriteLine("8. 列出每個學員上的每一堂課");
            {
                var studentCourseDetails = studentList
                                            .SelectMany((x) => x.CourseList, (student, courseId) => new { student, courseId })
                                            .Join(courseList, x => x.courseId, y => y.CourseId, (x, y) => new
                                            {
                                                StudentName = x.student.Name,
                                                CourseName = y.Name,
                                            });
                var result = studentCourseDetails.GroupBy((x) => x.StudentName)
                                                 .Select((y) => new
                                                 { 
                                                    StudentName = y.Key,
                                                    CourseName = string.Join(Environment.NewLine + "\t", y.Select((x) => x.CourseName))
                                                 });
                Console.WriteLine(string.Join(Environment.NewLine, result.Select((x) => $"{x.StudentName}" + "：" + Environment.NewLine + "\t" + $"{x.CourseName}")));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第9題
            // 9. 找出誰上的課數量最少
            Console.WriteLine("9. 找出誰上的課數量最少");
            {
                var courseCountRes = studentList.Select((x) => new 
                {
                    name = x.Name,
                    courseCount = x.CourseList.Count
                });
                Console.WriteLine(string.Join(Environment.NewLine, courseCountRes.Where((x) => x.courseCount == courseCountRes.Min((y) => y.courseCount))
                                                                                 .Select((x) => $"{x.name}：{x.courseCount} 門課")));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第10題
            // 10. 找出誰修的學分總和小於10
            Console.WriteLine("10. 找出誰修的學分總和小於10");
            {
                var studentCourseDetails = studentList
                            .SelectMany((x) => x.CourseList, (student, courseId) => new { student, courseId })
                            .Join(courseList, x => x.courseId, y => y.CourseId, (x, y) => new
                            {
                                StudentName = x.student.Name,
                                CourseName = y.Name,
                                Credit = y.Credit
                            });
                Console.WriteLine(string.Join(Environment.NewLine, studentCourseDetails.GroupBy((x) => x.StudentName).Where((y) => y.Sum((z) => z.Credit) < 10 ).Select((x) => x.Key + "：" + x.Sum((y) => y.Credit) + " 學分")));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第11題
            // 11. 找出誰最後獲得學分數最高
            Console.WriteLine("11. 找出誰最後獲得學分數最高");
            {
                var studentCourseDetails = studentList
                             .SelectMany((x) => x.CourseList, (student, courseId) => new { student, courseId })
                             .Join(courseList, x => x.courseId, y => y.CourseId, (x, y) => new
                             {
                                 StudentName = x.student.Name,
                                 Credit = y.Credit
                             });
                var newList = studentCourseDetails.GroupBy((x) => x.StudentName).Select((x) => new 
                            {
                                Name = x.Key,
                                Credit = x.Sum((y) => y.Credit)
                            });
                var result = newList.OrderByDescending((x) => x.Credit).FirstOrDefault();
                Console.WriteLine(string.Join(Environment.NewLine, $"{result.Name}：{result.Credit} 學分"));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第12題(加分題)
            // 12. 十二生肖自定義排序
            Console.WriteLine("12. 十二生肖自定義排序");
            {
                var zoo = new List<string> { "龍", "鼠", "馬", "豬", "羊" }; //答案: 鼠、龍、馬、羊、豬
                Console.WriteLine($"排序前: {string.Join("、", zoo)}{Environment.NewLine}");
                Console.Write("排序後: ");
                var orderByArr = new List<string>{ "鼠","牛","虎","兔","龍","蛇","馬","羊","猴","雞","狗","豬" };
                Console.WriteLine(string.Join("、", zoo.OrderBy((x) => orderByArr.IndexOf(x)).Select((x) => x)));
            }

            #endregion
            Console.ReadLine();
        }
    }
}
