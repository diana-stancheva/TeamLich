namespace AndOneConstructions.Controller
{
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.OleDb;
    using System.Collections.Generic;
    using System.Text;
    using Ionic.Zip;
    using MongoDB.Driver;
    using MongoDB.Data.Context;
    using AndOneConstructions.Model;
    using System.Data.Entity.Validation;

    public static class ImportDataController
    {
        public static void ImportMongoDBEmployees()
        {
            using (var db = new AndOneConstructionsContext())
            {
                var employee = new MongoDBEmployee();
                MongoCollection<MongoDBEmployee> employeeCollection = employee.GetAllEntitiesAsCollection();

                foreach (var emp in employeeCollection.FindAll())
                {
                    var isEmployeeInDB = db.Employees.Where(e => e.FirstName == emp.FirstName && e.LastName == emp.LastName);
                    
                    var isDepartmentInDB = db.Departments.Where(d => d.Name == emp.Department);

                    Console.WriteLine("empl {0}", isEmployeeInDB.Count());
                    Console.WriteLine("dep {0}", isDepartmentInDB.Count());

                    if (isEmployeeInDB.Count() == 0 && isDepartmentInDB.Count() == 0)
                    {
                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Department = new Department { Name = emp.Department }
                        });

                        db.SaveChanges();
                    }
                    else if (isEmployeeInDB.Count() == 0 && isDepartmentInDB.Count() > 0)
                    {
                        var departmentid = isDepartmentInDB.FirstOrDefault().DepartmentId;

                        db.Employees.Add(new Employee
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            DepartmentId = departmentid
                        });
                        db.SaveChanges();

                        Console.WriteLine("{0} added -> department id {1}", emp.FirstName + " " + emp.LastName, departmentid);
                    }
                    else
                    {
                        Console.WriteLine("Employee {0} Exists in DB", emp.FirstName + " " + emp.LastName);
                    }
                }
            }
        }

        public static void ImportDataFromExcel(string filePath)
        {
            var data = ReadExcelFile(filePath);
            var datareader = data.CreateDataReader();

            using (datareader)
            {
                while (datareader.Read())
                {
                    if (datareader["Project Name"] != DBNull.Value)
                    {
                        string projectName = (string)datareader["Project Name"];
                        DateTime projectStartDate = (DateTime)datareader["Start Date"];
                        DateTime projectEndDate = (DateTime)datareader["End Date"];
                        string projectClient = (string)datareader["Client"];
                        string buildingType = (string)datareader["Building Type"];
                        string buildingAddress = (string)datareader["Building Address"];
                        string buildingTown = (string)datareader["Building Town"];
                        string constructionSiteName = (string)datareader["Construction Site Name"];

                        using (var db = new AndOneConstructionsContext())
                        {
                            //TODO: Check for existing Project Name in DB
                            var isProjectInDB = db.Projects.Where(p => p.Name == projectName).Count() > 0;

                            //TODO: Check for existing Client in DB
                            var isClientInDB = db.Clients.Where(c => c.Name == projectClient).Count() > 0;

                            //TODO: Check for existing Building Type in DB
                            var isBuildigTypeInDB = db.Buildings.Where(b => b.Name == buildingType).Count() > 0;

                            //TODO: Check for existing Address in DB
                            //var isAddressInDB = db.Addresses.Where(a => a.Details == buildingAddress).Count() > 0;

                            //TODO: Check for existing Town in DB
                            var isTownInDB = db.Towns.Where(t => t.Name == buildingTown).Count() > 0;

                            //TODO: Check for existing Construction Site Name in DB
                            //var isConstructionSiteInDB = db.ConstructionSites.Where(cs => cs.Name == constructionSiteName).Count() > 0;

                            var project = new Project();

                            if (isProjectInDB)
                            {
                                Console.WriteLine("Project with name:  \"{0}\" exists In DB", projectName);
                            }
                            else 
                            {
                                project.Name = projectName;
                                project.StartDate = projectStartDate;
                                project.EndDate = projectEndDate;
                            }

                            if (isTownInDB)
                            {
                                Console.WriteLine("Town Exist In DB");
                            }

                            if (isClientInDB)
                            {
                                var clientID = db.Clients.Where(c => c.Name == projectClient).FirstOrDefault().ClientId;
                                project.ClientId = clientID;
                            }
                            else
                            {
                                project.Client = new Client
                                {
                                    Name = projectClient
                                };
                            }

                            if (!isTownInDB && !isBuildigTypeInDB)
                            {
                                project.ConstructionSites = new HashSet<ConstructionSite>
                                {
                                    new ConstructionSite
                                    {
                                        Name = constructionSiteName,
                                        Address = new Address
                                        {
                                            Details = buildingAddress,
                                            Town = new Town {
                                                Name = buildingTown
                                            }
                                        },
                                        Buildings = new HashSet<Building>
                                        {
                                            new Building
                                            {
                                                Name = buildingType
                                            }
                                        }
                                    }
                                };
                            }
                            else if (isTownInDB && !isBuildigTypeInDB)
                            {
                                var townID = db.Towns.Where(t => t.Name == buildingTown).FirstOrDefault().TownId;
                                //var building = db.Buildings.Where(b=>b.Name == buildingType).FirstOrDefault();

                                project.ConstructionSites = new HashSet<ConstructionSite>
                                {
                                    new ConstructionSite
                                    {
                                        Name = constructionSiteName,
                                        Address = new Address
                                        {
                                            Details = buildingAddress,
                                            TownId = townID
                                        },
                                        Buildings = new HashSet<Building>
                                        {
                                            new Building
                                            {
                                                Name = buildingType
                                            }
                                        }
                                    }
                                };
                            }
                            else if (!isTownInDB && isBuildigTypeInDB)
                            {
                                var building = db.Buildings.Where(b=>b.Name == buildingType).FirstOrDefault();

                                project.ConstructionSites = new HashSet<ConstructionSite>
                                {
                                    new ConstructionSite
                                    {
                                        Name = constructionSiteName,
                                        Address = new Address
                                        {
                                            Details = buildingAddress,
                                            Town = new Town {
                                                Name = buildingTown
                                            }
                                        },
                                        Buildings = new HashSet<Building>
                                        {
                                            building
                                        }
                                    }
                                };
                            }
                            else if(isTownInDB && isBuildigTypeInDB)
                            {
                                var townID = db.Towns.Where(t => t.Name == buildingTown).FirstOrDefault().TownId;
                                var building = db.Buildings.Where(b=>b.Name == buildingType).FirstOrDefault();

                                project.ConstructionSites = new HashSet<ConstructionSite>
                                {
                                    new ConstructionSite
                                    {
                                        Name = constructionSiteName,
                                        Address = new Address
                                        {
                                            Details = buildingAddress,
                                            TownId = townID
                                        },
                                        Buildings = new HashSet<Building>
                                        {
                                            building
                                        }
                                    }
                                };
                            }

                            db.Projects.Add(project);

                            //TODO: Find this error
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException dbEx)
                            {
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        Console.WriteLine("\nProperty: {0} Error: {1}\n", validationError.PropertyName, validationError.ErrorMessage);
                                    }
                                }
                            }

                            Console.WriteLine("Project {0} Added", projectName);
                        }
                    }
                }
            }
        }

        public static void ReadDataFromXLSX(string filePath)
        {
            var data = ReadExcelFile(filePath);

            var datareader = data.CreateDataReader();

            using (datareader)
            {
                int counter = 1;

                while (datareader.Read())
                {
                    if (datareader["Project Name"] != DBNull.Value)
                    {
                        string name = (string)datareader["Project Name"];
                        DateTime date = (DateTime)datareader["Start Date"];
                        Console.WriteLine("{0}. Name: {1}, Start Date: {2}", counter, name, date);

                        counter++;
                    }
                }
            }
        }

        public static void ExtractZipFile(string zipPath, string filePath)
        {
            //EXTRACTS ALL ARCHIVE
            //using (ZipFile zip = ZipFile.Read("../../../Projects-Reports.zip"))
            //{
            //    zip.ExtractAll("../../../");
            //    Console.WriteLine("Zip Extracted");
            //}
            //EXTRACTS ZIP - FILE BY FILE
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                foreach (ZipEntry e in zip)
                {
                    if (e.FileName == filePath)
                    {
                        e.Extract("../../../", ExtractExistingFileAction.OverwriteSilently);
                        Console.WriteLine("{0} Extracted", e.FileName);
                    }
                }
            }
        }

        private static DataSet ReadExcelFile(string filePath)
        {
            DataSet ds = new DataSet();

            string connectionString = GetConnectionString(filePath);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();

                    if (!sheetName.EndsWith("$"))
                        continue;

                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    ds.Tables.Add(dt);
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }

        private static string GetConnectionString(string filePath)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = filePath;

            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "../../../Projects-Reports/12-Jul-2014/Projects-Sofia-Report.xlsx";

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
    }
}