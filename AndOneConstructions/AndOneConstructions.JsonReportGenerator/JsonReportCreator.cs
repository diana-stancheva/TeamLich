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
  

        public static void GenerateJson(int projectId, string name, DateTime? startDate, DateTime? endDate)
        {
            object reportData = new
            {
                ProjectId = projectId,
                Name = name,
                StartDate = string.Format("{0: dd-MMM-yyyy}", startDate),
                EndDate = string.Format("{0: dd-MMM-yyyy}", endDate)
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
