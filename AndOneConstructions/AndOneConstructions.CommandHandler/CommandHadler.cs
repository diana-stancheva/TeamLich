namespace AndOneConstructions.CommandHandler
{
    using AndOneConstructions.Controller;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandHadler
    {
        private ImportDataController importDataController;
        private ExportDataController exportDataController;

        public CommandHadler(ImportDataController importDataController, ExportDataController exportDataController)
        {
            this.importDataController = importDataController;
            this.exportDataController = exportDataController;
        }

        public void Run()
        {
            StartScreen();

            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        importDataController.ZIPImport();
                        StartScreen();
                        break;

                    case "2":
                        importDataController.ImportMongoDBEmployees();
                        StartScreen();
                        break;

                    case "3":
                        exportDataController.ExportPdfReport(2013);
                        StartScreen();
                        break;

                    case "4":
                        exportDataController.CreateJsonReport();
                        StartScreen();
                        break;

                    case "5":
                        exportDataController.ExportXMLReport();
                        StartScreen();
                        break;

                    case "6":
                        exportDataController.ExportDataToMySql();
                        StartScreen();
                        break;

                    case "7":
                        importDataController.ImportXMLToMongo("../../projectsEmpl.xml");
                        break;

                    case "8": exportDataController.CreateReporToExcell();
                        StartScreen();
                        break;

                    default:
                        break;
                }
            }

        }

        private void StartScreen()
        {
            Console.WriteLine("\nWaiting for a command\n");
            Console.WriteLine("1 - Import Data From Zip Archive With Excel Files");
            Console.WriteLine("2 - Import Data From MongoDB");
            Console.WriteLine("3 - Export Projects To PDF File");
            Console.WriteLine("4 - Export Projects To .json File");
            Console.WriteLine("5 - Export Projects To XML File");
            Console.WriteLine("6 - Export Projects To MySql Database");
            Console.WriteLine("7 - Import Rojects From XML To MongoDB");
            Console.WriteLine("8 - Export Projects With Costs In XLS File");
        }
    }
}
