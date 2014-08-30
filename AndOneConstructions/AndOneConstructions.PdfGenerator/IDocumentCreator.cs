namespace AndOneConstructions.PdfGenerator
{
    using MigraDoc.DocumentObjectModel;

    public interface IDocumentCreator
    {
        Document CreateDocument();
    }
}
