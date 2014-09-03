﻿/*
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
 * За връзка с MySQL през Workbench
 *
 * Hostname 	d93ab522-6908-4034-9827-a396016feca4.mysql.sequelizer.com
 * Username 	omuysinjdtwtmbue
 * Password 	dxSZ7cUWJ7q7PYXKxv855mVAWi62K6nPRDGdbBzERbvUeHQq6qvhd2oYKqdWe3kL
 *
 *
 * За връзка към MongoDB
 *
 * mongodb://teamlich:teamlich@ds063929.mongolab.com:63929/appharbor_f580ae0d-6ef8-4aac-b142-db0920bfddac
 *
 */

namespace AndOneConstructions.ConsoleClient
{
    using AndOneConstructions.Controller;
    using AndOneConstructions.Model;
    using MongoDB.Data.Context;
    using SecretDB.Data;
    using SecretDB.Models;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using AndOneConstructions.XMLReader;

    public class EntryPoint
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            StartScreen();

            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        //Console.Clear();
                        ZIPImport();
                        StartScreen();
                        break;

                    case "2":
                        //Console.Clear();
                        ImportDataController.ImportMongoDBEmployees();
                        StartScreen();
                        break;

                    case "3":
                        //Console.Clear();
                        ExportDataController.ExportPdfReport(2013);
                        StartScreen();
                        break;

                    case "4":
                        ExportDataController.CreateJsonReport();
                        StartScreen();
                        break;

                    case "5":
                        ExportDataController.ExportXMLReport();
                        StartScreen();
                        break;

                    case "6": ExportDataController.ExportDataToMySql();
                        StartScreen();
                        break;

                    default:
                        break;
                }
            }

            // Test SQL
            // TestSQL();

            // Test Mongo
            // TestMongo();

            // Test SQLite
           // TestSQLite();
        }

        public static void StartScreen()
        {
            Console.WriteLine("\n Waiting for a command\n");
            Console.WriteLine("1 - Import Data From Zip Archive With Excel Files");
            Console.WriteLine("2 - Import Data From MongoDB");
            Console.WriteLine("3 - Export Projects To PDF File");
            Console.WriteLine("4 - Export Projects To .json File");
            Console.WriteLine("5 - Export Projects To XML File");
            Console.WriteLine("6 - Export Projects To MySql Database");
        }

        public static void ZIPImport()
        {
            ////TEST IMPORTING FROM EXCEL TO SQL
            ////Folder with extracted files (Projects-Reports) will be deleted after importing data to excel
            ImportDataController.ExtractZipFile("../../../Projects-Reports.zip", "Projects-Reports/12-Jul-2014/Projects-Sofia-Report.xlsx");
            ImportDataController.ImportDataFromExcel("../../../Projects-Reports/12-Jul-2014/Projects-Sofia-Report.xlsx");

            ImportDataController.ExtractZipFile("../../../Projects-Reports.zip", "Projects-Reports/12-Jul-2014/Projects-Varna-Report.xlsx");
            ImportDataController.ImportDataFromExcel("../../../Projects-Reports/12-Jul-2014/Projects-Varna-Report.xlsx");

            ImportDataController.ExtractZipFile("../../../Projects-Reports.zip", "Projects-Reports/12-Jul-2014/Projects-Burgas-Report.xlsx");
            ImportDataController.ImportDataFromExcel("../../../Projects-Reports/12-Jul-2014/Projects-Burgas-Report.xlsx");

            ImportDataController.ExtractZipFile("../../../Projects-Reports.zip", "Projects-Reports/12-Jul-2014/Projects-Plovdiv-Report.xlsx");
            ImportDataController.ImportDataFromExcel("../../../Projects-Reports/12-Jul-2014/Projects-Plovdiv-Report.xlsx");
        }

        private static void TestSQL()
        {
            //TEST SQL
            var db = new AndOneConstructionsContext();
            var buildings = db.Buildings.ToList();
            // for testing
            foreach (var b in buildings)
            {
                Console.WriteLine(b.Name);
            }
        }

        private static void TestMongo()
        {
            // TEST MONGO
            var test = new MongoDBEmployee();
            test.PrintAllEntities();
            //Test Mongo Import
            ImportDataController.ImportMongoDBEmployees();
            using (var db = new AndOneConstructionsContext())
            {
                var employees = db.Employees.ToList();
                foreach (var item in employees)
                {
                    Console.WriteLine(item.FirstName + ' ' + item.LastName);
                }
            }

            ExportDataController.ExportPdfReport(2013);
        }

        private static void TestSQLite()
        {
            SecretDBContext db = new SecretDBContext();

            var br = new Bribe();
            br.Amount = 15000;
            br.Description = "YEAH!!!!!!!!";
            br.LastUpdate = DateTime.Now;
            br.ProjectName = "TEst";
            br.ProjectId = 1;

            db.Bribes.Add(br);

            Console.WriteLine(db.SaveChanges());

            var all = db.Bribes.Where(x => x.BribeId > 0).ToList();

            foreach (var item in all)
            {
                Console.WriteLine(
                    item.BribeId + " " +
                    item.Amount + " " +
                    item.Description + " " +
                    item.ProjectName + " " +
                    item.ProjectId + " " +
                    item.LastUpdate
                    );
            }
        }

        // should be deleted just for testing
        private static void TestXMLReader()
        {
            var xmlReader = new XElementProjectReader();
            var result = xmlReader.Read("../../projectsEmpl.xml");
        }
    }
}