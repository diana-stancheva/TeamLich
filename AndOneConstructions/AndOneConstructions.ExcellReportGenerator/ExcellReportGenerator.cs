namespace AndOneConstructions.ExcellReportGenerator
{
    using System;

    using AndOneConstructions.MySqlModel;
    using System.Data.SQLite;
    using System.Data.SqlClient;
    using ExcelLibrary.Office.Excel;
    using Spire.Xls;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;

    public class ExcellReportGenerator
    {
        public static void Export(AndOneConstructionEntitiesModel context)
        {
            string connectionStr = "Data Source=../../../AndOneConstructions.ExcellReportGenerator/SQLiteDB.s3db;";
            SQLiteConnection dbSQLiteConnection = new SQLiteConnection(connectionStr);
            dbSQLiteConnection.Open();

            List<ReportInfo> report = new List<ReportInfo>();

            using (dbSQLiteConnection)
            {
                using (context)
                {
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM Projects", dbSQLiteConnection);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var projectID =(long)reader["ProjectID"];
                        var projectCost = (long)reader["ProjectCost"];
                        var projectName = (string)reader["ProjectName"];

                        var projectByID = context.Projects
                        .FirstOrDefault(p => p.ProjectId == projectID);
                        var projectStartDate = (DateTime)projectByID.StartDate;

                        report.Add(new ReportInfo(projectID, projectName, projectCost, projectStartDate));
                    }
                }
            }

            CreateExcellReport(report);
            
        }

        internal static void CreateExcellReport(List<ReportInfo> report)
        {
            //create new xls file
            //Initialize a new Workboook object
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            //Get the first worksheet
            Spire.Xls.Worksheet sheet = workbook.Worksheets[0];
            //column headers
            sheet.Range[1,1].Text = "ProjectID";
            sheet.Range[1,2].Text = "Name";
            sheet.Range[1,3].Text = "Cost";
            sheet.Range[1, 4].Text = "EndDate";
            sheet.SetColumnWidth(2, 30);
            sheet.SetColumnWidth(3, 12);
            sheet.SetColumnWidth(4, 12);

            //Initialize data
            for (int i = 0; i < report.Count; i++)
            {
                sheet.Range[i+2, 1].NumberValue = report[i].id;
                sheet.Range[i+2, 2].Text = report[i].name;
                sheet.Range[i+2, 3].NumberValue = report[i].cost;
                sheet.Range[i+2, 4].DateTimeValue = report[i].startDate;
            }

            //Save workbook to disk
            workbook.SaveToFile("Sample.xls");
            try
            {
                System.Diagnostics.Process.Start(workbook.FileName);
            }
            catch { }
        }

        internal class ReportInfo
        {
            internal long id;
            internal string name;
            internal long cost;
            internal DateTime startDate;

            public ReportInfo(long id, string name, long cost, DateTime date)
            {
                this.id = id;
                this.name = name;
                this.cost = cost;
                this.startDate = date;
            }
        }
    }
}