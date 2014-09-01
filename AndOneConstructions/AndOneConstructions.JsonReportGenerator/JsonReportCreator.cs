namespace AndOneConstructions.JsonReportGenerator
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    using AndOneConstructions.Model;

    public class JsonReportCreator
    {
        private const string ReportFolder = "../../JsonReports/";
        private const string FileName = "{0}.json";
        private AndOneConstructionsContext db = new AndOneConstructionsContext();

        public void CreateReport()
        {
            var projects = this.db.Projects;

            foreach (var project in projects)
            {
                var projectId = project.ProjectId;
                var name = project.Name;
                var startDate = project.StartDate;
                var endDate = project.EndDate;

                this.GenerateJson(projectId, name, startDate, endDate);
            }
        }

        private void GenerateJson(int projectId, string name, DateTime? startDate, DateTime? endDate)
        {
            object reportData = new
            {
                ProjectId = projectId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate
            };

            JsonSerializer serializer = new JsonSerializer();
            string projectAsJson = JsonConvert.SerializeObject(reportData, Formatting.Indented);

            if (!Directory.Exists(ReportFolder))
            {
                Directory.CreateDirectory(ReportFolder);
            }

            StreamWriter writer = new StreamWriter(ReportFolder + string.Format(FileName, projectId.ToString()));
            using (writer)
            {
                writer.WriteLine(projectAsJson);
            }
        }
    }
}
