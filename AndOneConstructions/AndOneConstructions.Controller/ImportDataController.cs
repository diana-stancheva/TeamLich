namespace AndOneConstructions.Controller
{
    using System;
    using System.Linq;

    using MongoDB.Data.Context;
    using MongoDB.Driver;

    using AndOneConstructions.Model;

    public static class ImportDataController
    {
        public static void ImportMongoDBEmployees()
        {
            using (var db = new AndOneConstructionsContext())
            {
                var employee = new MongoDBEmployee();
                MongoCollection<MongoDBEmployee> employeeCollection = employee.GetAllEntitiesAsCollection();

                foreach (var emp in employeeCollection.FindAll())
                {
                    var isEmployeeInDB = db.Employees.Where(e => e.FirstName == emp.FirstName && e.LastName == emp.LastName);
                    
                    var isDepartmentInDB = db.Departments.Where(d => d.Name == emp.Department);

                    Console.WriteLine("empl {0}", isEmployeeInDB.Count());
                    Console.WriteLine("dep {0}", isDepartmentInDB.Count());

                    if (isEmployeeInDB.Count() == 0 && isDepartmentInDB.Count() == 0)
                    {
                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Department = new Department { Name = emp.Department }
                        });

                        db.SaveChanges();
                    }
                    else if (isEmployeeInDB.Count() == 0 && isDepartmentInDB.Count() > 0)
                    {
                        var departmentid = isDepartmentInDB.FirstOrDefault().DepartmentId;

                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            DepartmentId = departmentid
                        });
                        db.SaveChanges();


                        Console.WriteLine("{0} added -> department id {1}", emp.FirstName + " " + emp.LastName, departmentid);
                    }
                    else
                    {
                        Console.WriteLine("Employee {0} Exists in DB", emp.FirstName + " " + emp.LastName);
                    }
                }
            }
        }

        public static void ImportMongoDBProjects()
        {
            using (var db = new AndOneConstructionsContext())
            {
                
            }
        }

        public static void ImportDataFromZIP()
        {
            throw new NotImplementedException();
        }
    }
}
