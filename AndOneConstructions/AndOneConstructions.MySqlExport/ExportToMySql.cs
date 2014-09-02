namespace AndOneConstructions.MySqlExport
{
    using System;

    using AndOneConstructions.MySqlModel;
    public class ExportToMySql
    {
        public static void Export(AndOneConstructionEntitiesModel context, int projectId, string projectName, DateTime? startDate, DateTime? endDate)
        {
            var projects = context.Projects;

            if (projects != null)
            {
                Project newProject = new Project
                {
                    ProjectId = projectId,
                    Name = projectName,
                    StartDate = startDate,
                    EndDate = endDate
                };

                context.Add(newProject);
                context.SaveChanges();
            }
        }
    }
}
