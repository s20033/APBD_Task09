using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }

        /*
            The purpose of the exercise is to implement the following methods.
            Each method should contain C# code, which with the help of LINQ will perform queries described using SQL.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Task1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

            var res1 = Emps.Where(e => e.Job == "Backend programmer");
            //2. Lambda and Extension methods
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Task2()
        {
            var res = Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
              .OrderByDescending(emp => emp.Ename)
              .Select(emp => new

              {
                  Name = emp.Ename,
                  employeejob = emp.Job
              }
            );
            res.ToList().ForEach(Console.WriteLine);



        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Task3()
        {
            var res = Emps.Max(emp => emp.Salary);
            Console.WriteLine(res);

           
        } 

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Task4()
        {
            
            var res = Emps.Where(emp => emp.Salary == Emps.Max(emp => emp.Salary))

                .Select(emp => new

                {
                    Name = emp.Ename,
                    salary = emp.Salary
                }
             );

            res.ToList().ForEach(Console.WriteLine);
        }

        /// <summary>
        /// SELECT ename AS FirstName, job AS EmployeeJob FROM Emps;
        /// </summary>
        public void Task5()
        {
            //1. SQL method
            var res1 = from emp in Emps
                       select new
                       {
                           FirstName = emp.Ename,
                           EmployeeJob = emp.Job
                       };
            //2. Lambda and Extension method
            var res2 = Emps.Select(emp => new
            {
                FirstName = emp.Ename,
                EmployeeJob = emp.Job
            });
            res1.ToList().ForEach(Console.WriteLine);
            res2.ToList().ForEach(Console.WriteLine);


        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Result: Joining collections Emps and Depts.
        /// </summary>
        public void Task6()
        {
            //SQL method
            var res1 = from emp in Emps
                       join dept in Depts on emp.Deptno equals dept.Deptno
                       select new
                       {
                           emp.Ename,
                           emp.Job,
                           dept.Dname
                       };
            //2. Lambda and Extension method
            var res2 = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (e, d) => new { e.Ename, e.Job, d.Dname });
            res1.ToList().ForEach(Console.WriteLine);
            res2.ToList().ForEach(Console.WriteLine);

        }

        /// <summary>
        /// SELECT Job AS EmployeeJob, COUNT(1) EmployeeNumber FROM Emps GROUP BY Job;
        /// </summary>
        public void Task7()
        { //1. SQL method
            var res1= from emp in Emps
                      group emp by emp.Job into Job
                      select new
                      {
                          EmployeeJob=Job.Key,
                          EmployeeNumber=Job.Count()
                      };
            //2.lambda and Extension method
            var res2 = Emps.GroupBy(emp => emp.Job)
                .Select(emp => new
                {
                    EmployeeJob = emp.Key,
                    EmployeeNumber = emp.Count()
                });
            
        }

        /// <summary>
        /// Return value "true" if at least one of 
        /// the elements of collection works as "Backend programmer".
        /// </summary>
        public bool Task8()
        {
            //1. SQL method
            var res1= (from emp in Emps
                        where emp.Job == "Backend programmer" select emp).Count() > 0;
           
            //2. lambda and Extension method
            var res2 = Emps.Any(emp => emp.Job == "Backend programmer");
            if (res1 != null && res2!=null)
            {
                return true;
            }
            return false;

            


        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Task9()
        {
            var res = Emps.Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .FirstOrDefault();
            
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "No value", null, null;
        /// </summary>
        public void Task10()
        {
            var res = Emps.Select(emp => new
            {
                emp.Ename,
                emp.Job,
                emp.HireDate
            }).Union(new[] { new { Ename = "No value", Job = (string)null, HireDate = (DateTime?)null } });

            res.ToList().ForEach(Console.WriteLine);

        }

        //Find the employee with the highest salary using the Aggregate () method
        public void Task11()
        {
            var res = Emps.Aggregate((emp1, emp2) => emp1.Salary > emp2.Salary ? emp1 : emp2);

        }

        //Using the LINQ language and the SelectMany method, 
        //perform a CROSS JOIN join between collections Emps and Depts
        public void Task12()
        {
            //1. SQl method
            var res1 = from emp in Emps
                        from dept in Depts
                        select new
                        {
                            emp.Empno,
                            emp.Ename,
                            emp.Job,
                            emp.Mgr,
                            emp.Salary,
                            emp.HireDate,
                            dept.Deptno,
                            dept.Dname,
                            dept.Loc
                        };
            //2. lambda and Extension method
            var res2 = Emps.SelectMany(emp => Depts, (emp, dept) => new
            {
                emp.Empno,
                emp.Ename,
                emp.Job,
                emp.Mgr,
                emp.Salary,
                emp.HireDate,
                dept.Deptno,
                dept.Dname,
                dept.Loc
            });
            res1.ToList().ForEach(Console.WriteLine);
            res2.ToList().ForEach(Console.WriteLine);
        }
    }
}
