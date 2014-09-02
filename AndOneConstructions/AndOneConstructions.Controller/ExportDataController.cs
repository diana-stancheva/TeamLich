﻿namespace AndOneConstructions.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AndOneConstructions.Model;
    using AndOneConstructions.PdfGenerator;
    using AndOneConstructions.JsonReportGenerator;

    public static class ExportDataController
    {
        private static AndOneConstructionsContext db = new AndOneConstructionsContext();

        public static void CreateJsonReport()
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

        public static void ExportPdfReport(int year)
        {
            using (db)
            {
                var months = new List<string>();
                var numberOfProjects = new List<double>();

                var projectsGropedByStartMonth = db.Projects
                                                   .Where(p => p.StartDate.Value.Year == year)
                                                   .GroupBy(p => p.StartDate.Value.Month);

                var sorted = projectsGropedByStartMonth.ToList()
                                                       .Select(g => new
                                                       {
                                                           Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(g.Key),
                                                           Count = g.Count()
                                                       })
                                                       .OrderBy(x => DateTime.ParseExact(x.Month, "MMMM", CultureInfo.InvariantCulture));

                foreach (var group in sorted)
                {
                    months.Add(group.Month);
                    numberOfProjects.Add((double)group.Count);
                }

                var chartCreator = new ChartCreator(numberOfProjects.ToArray(), months.ToArray(), year);
                var pdfRenderer = new DocumentRenderer();

                pdfRenderer.Document = chartCreator.CreateDocument();

                // see bin directory
                pdfRenderer.Render("ProjectChart");

                Console.WriteLine("\nPDF File created!");
                Console.WriteLine("Open PDF File? yes/no");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "yes":
                        System.Diagnostics.Process.Start(@"ProjectChart.pdf");
                        break;
                        default
                        : break;
                }
            }
        }
    }
}