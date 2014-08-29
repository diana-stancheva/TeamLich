/*
 * 
 * АКО НЯКОЙ НЕ Е ПРОЧЕЛ В SKYPE:
 * 
 * Използваме хостинга на App Harbor за всички database сървъри,
 * за да нямаме проблеми с coonnection стринговете и с различните версии на SQL SERVER
 * и за да не се налага да прехвърляме постоянно бази данни през github.
 * 
 * Тествала съм, според мен работи добре, вижте дали всичко е наред при вас
 * 
 * Контекста за работа с SQL базата в App Harbor e:
 * AndOneConstructions.Model.AndOneConstructionsContext()
 * 
 * AndOneConstructions.Data.AndOneConstructionsDataContext() - > Работи с локалната база данни
 * /и ако не сте с SQLEXPRESS 2014 трябва да промените connection стринга в app.config/
 * Оставила съм го за сега, когато видим, че всичко е ок с App Harbor ще го махнем.
 * 
 * За връзка с SQL през Management Studio:
 * 
 * Server:      d80b5849-dd81-49dc-acdc-a396015983af.sqlserver.sequelizer.com
 * Login:     	rqhqhjzlydkhrstg 	
 * Password: 	6vULZxqG4R6jrzudTKdpEpKo6o5EnDiztsPaDvBqvTTGzUkFVuFfNERQSEPLATux
 * 
 * 
 */
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
            //TEST SQL 
            var db = new AndOneConstructionsContext();

            var buildings = db.Buildings.ToArray();

            // for testing
            foreach (var b in buildings)
            {
                Console.WriteLine(b.Name);
            }

            //TEST MONGO
            var test = new MongoDBEmployee();
            test.PrintAllEntities();

            ////Test Mongo Import

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
