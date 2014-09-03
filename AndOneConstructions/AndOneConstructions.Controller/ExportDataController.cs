namespace AndOneConstructions.Controller
{
    using AndOneConstructions.ExcellReportGenerator;
    using AndOneConstructions.JsonReportGenerator;
    using AndOneConstructions.Model;
    using AndOneConstructions.MySqlExport;
    using AndOneConstructions.MySqlModel;
    using AndOneConstructions.PdfGenerator;
    using AndOneConstructions.XMLGenerator;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    public class ExportDataController
    {
        private AndOneConstructionsContext db = new AndOneConstructionsContext();
        private AndOneConstructionEntitiesModel context = new AndOneConstructionEntitiesModel();

        public void CreateJsonReport()
        {
            var projects = db.Projects;

            foreach (var project in projects)
            {
                var projectId = project.ProjectId;
                var name = project.Name;
                var startDate = project.StartDate;
                var endDate = project.EndDate;

                JsonReportCreator.GenerateJson(projectId, name, startDate, endDate);
            }
        }

        public void ExportPdfReport(int year)
        {
            using (db)
            {
                var months = new List<string>();
                var numberOfProjects = new List<double>();

                var projectsGropedByStartMonth = db.Projects
                                                   .Where(p => p.StartDate.Value.Year == year)
                                                   .GroupBy(p => p.StartDate.Value.Month);

                var sortedByMonthsName = projectsGropedByStartMonth.ToList()
                                                       .Select(g => new
                                                       {
                                                           Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(g.Key),
                                                           Count = g.Count()
                                                       })
                                                       .OrderBy(x => DateTime.ParseExact(x.Month, "MMMM", CultureInfo.InvariantCulture));

                foreach (var group in sortedByMonthsName)
                {
                    months.Add(group.Month.Substring(0, 3));
                    numberOfProjects.Add((double)group.Count);
                }

                var chartCreator = new ChartCreator(numberOfProjects.ToArray(), months.ToArray(), year);
                var pdfRenderer = new DocumentRenderer();

                pdfRenderer.Document = chartCreator.CreateDocument();

                // see bin directory
                pdfRenderer.Render("../../ProjectChart");

                Console.WriteLine("\nPDF File created!");
                Console.WriteLine("Open PDF File? yes/no");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "yes":
                        Process.Start(@"..\..\ProjectChart.pdf");
                        break;
                        default
                        : break;
                }
            }
        }

        public void ExportXMLReport()
        {
            using (var db = new AndOneConstructionsContext())
            {
                var projects = db.Projects.ToArray();

                var xmlGenerator = new XElementProjectGenerator(projects);

                xmlGenerator.Generate("../../projects.xml");

                Console.WriteLine("XML exported.");
            }
        }

        public void ExportDataToMySql()
        {
            var projects = db.Projects;

            foreach (var project in projects)
            {
                var projectId = project.ProjectId;
                var name = project.Name;
                var startDate = project.StartDate;
                var endDate = project.EndDate;

                ExportToMySql.Export(context, projectId, name, startDate, endDate);
            }
        }

        public void CreateReporToExcell()
        {
            ExcellReportGenerator.Export(context);
        }
    }
}