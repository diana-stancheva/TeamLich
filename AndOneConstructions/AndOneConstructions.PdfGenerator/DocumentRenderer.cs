namespace AndOneConstructions.PdfGenerator
{
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DocumentRenderer : IRenderer
    {
        public Document Document { get; set; }

        private void InternalRender(string fileName)
        {
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);

            renderer.Document = this.Document;
            renderer.RenderDocument();

            // Save the document in bin directory
            renderer.PdfDocument.Save(fileName + ".pdf");
        }

        public void Render(string fileName)
        {
            InternalRender(fileName);
        }
    }
}
