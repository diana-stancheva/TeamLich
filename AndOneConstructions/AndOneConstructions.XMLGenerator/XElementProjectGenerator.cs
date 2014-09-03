namespace AndOneConstructions.XMLGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using AndOneConstructions.Model;

    public class XElementProjectGenerator : IXMLGenerator
    {
        private IEnumerable<Project> projects;

        public XElementProjectGenerator(IEnumerable<Project> projects)
        {
            this.projects = projects;
        }

        protected virtual XElement GenerateReportStructure()
        {
            XNamespace xmlNamespace = "http://andoneconstructions.bg/";

            XElement report = new XElement(XName.Get("projects", "http://andoneconstructions.bg/"));

            foreach (var project in this.projects)
            {
                var xmlProject = new XElement(xmlNamespace + "project");
                xmlProject.SetAttributeValue("name", project.Name);
                xmlProject.SetAttributeValue("client", project.Client.Name);

                foreach (var employee in project.Employees)
                {
                    var xmlEmployee = new XElement(xmlNamespace + "employee");
                    xmlEmployee.SetAttributeValue("first-name", employee.FirstName);
                    xmlEmployee.SetAttributeValue("last-name", employee.LastName);

                    xmlProject.Add(xmlEmployee);
                }

                report.Add(xmlProject);
            }

            
            return report;
        }

        public void Generate(string fileName)
        {
            var report = this.GenerateReportStructure();

            report.Save(fileName);
        }
    }
}
