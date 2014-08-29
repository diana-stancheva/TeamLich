namespace AndOneConstructions.ConsoleClient
{
    using AndOneConstructions.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Data.Context;

    using AndOneConstructions.Controller;
    using AndOneConstructions.Data;
    using AndOneConstructions.Model;

    class EntryPoint
    {
        static void Main()
        {
            var db = new AndOneConstructionsContext();

            var buildings = db.Buildings.ToArray();

            // for testing
            foreach (var b in buildings)
            {
                Console.WriteLine(b.Name);
            }

            ////TEST MONGO
            //var test = new MongoDBEmployee();
            //test.PrintAllEntities();

            //Test Mongo Import

            //ImportDataController.ImportMongoDBEmployees();

            //using (var db = new AndOneConstructionsContext())
            //{
            //    var employees = db.Employees.ToList();
            //    foreach (var item in employees)
            //    {
            //        Console.WriteLine(item.FirstName + ' ' + item.LastName);
            //    }
            //}

        }
    }
}
