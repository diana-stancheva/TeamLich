namespace AndOneConstructions.Controller
{
    
    using System;
    using System.Linq;

    using MongoDB.Data.Context;
    using AndOneConstructions.Model;
    using MongoDB.Driver;

    public static class ImportDataController
    {
        public static void ImportMongoDBEmployees()
        {
            using (var db = new AndOneConstructionsContext() )
            {
                var employee = new MongoDBEmployee();
                MongoCollection<MongoDBEmployee> employeeCollection = employee.GetAllEntities();

                foreach (var emp in employeeCollection.FindAll())
                {
                    var isInDB = db.Employees.Select(e => e.FirstName == emp.FirstName && e.LastName == emp.LastName).FirstOrDefault();

                    if (isInDB == null)
                    {
                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Department = new Department { Name = emp.Department}
                        });
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
