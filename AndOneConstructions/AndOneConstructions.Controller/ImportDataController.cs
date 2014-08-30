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
            using (var db = new AndOneConstructionsContext() )
            {
                var employee = new MongoDBEmployee();
                MongoCollection<MongoDBEmployee> employeeCollection = employee.GetAllEntitiesAsCollection();

                foreach (var emp in employeeCollection.FindAll())
                {
                    var isInDB = db.Employees.Where(e => e.FirstName == emp.FirstName && e.LastName == emp.LastName);

                    //TODO: Check for Department!!!

                    if (isInDB.Count() == 0)
                    {
                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Department = new Department { Name = emp.Department }
                        });

                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Employee {0} Exists in DB", emp.FirstName + " " + emp.LastName);
                    }
                }
            }
        }

        public static void ImportDataFromZIP()
        {
            throw new NotImplementedException();
        }
    }
}
