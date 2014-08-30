namespace AndOneConstructions.Controller
{
    using AndOneConstructions.Model;
    using AndOneConstructions.PdfGenerator;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class ExportDataController
    {
        public static void ExportPdfReport(int year)
        {
            using (var db = new AndOneConstructionsContext())
            {
                var months = new List<string>();
                var numberOfProjects = new List<double>();

                var projectsGropedByStartMonth = db.Projects.Where(p => p.StartDate.Value.Year == year)
                                                            .GroupBy(p => p.StartDate.Value.Month);

                var sorted = projectsGropedByStartMonth.ToList()
                    .Select(g =>
                        new
                        {
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                            Count = g.Count()
                        }
                     )
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
            }
        }
    }
}
