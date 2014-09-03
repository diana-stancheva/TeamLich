namespace AndOneConstructions.XMLReader
{
    using AndOneConstructions.Model;
    using System.Collections.Generic;

    public interface IReader
    {
        IEnumerable<Project> Read(string fileName);
    }
}
