namespace AndOneConstructions.XMLReader
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AndOneConstructions.Model;
    using System.Xml.Linq;

    public class XElementProjectReader : IReader
    {
        public IEnumerable<Project> Read(string fileName)
        {
            HashSet<Project> result = new HashSet<Project>();

            XDocument xmlDoc = XDocument.Load(fileName);
            var projects = xmlDoc.Descendants("project");

            foreach (var xmlProject in projects)
            {
                var project = new Project();
                project.Name = xmlProject.Attribute("name").Value;
                project.Client = new Client();
                project.Client.Name = xmlProject.Attribute("client").Value;

                var departmentsAndEmployees = xmlProject.Descendants("department")
                               .Select(d => new
                               {
                                   DepartmentName = d.Attribute("name").Value,
                                   EmplsInDepartment = d.Descendants("employee")
                                                        .Select(e => new 
                                                        { 
                                                            FirstName = e.Attribute("first-name").Value,
                                                            LastName = e.Attribute("last-name").Value
                                                        })
                               });

                foreach (var department in departmentsAndEmployees)
                {
                    foreach (var emp in department.EmplsInDepartment)
                    {
                        var employee = new Employee();
                        employee.Department = new Department();
                        employee.Department.Name = department.DepartmentName;
                        employee.FirstName = emp.FirstName;
                        employee.LastName = emp.LastName;

                        project.Employees.Add(employee);
                    }
                }

                result.Add(project);
            }

            return result;
        }
    }
}
