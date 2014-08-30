namespace AndOneConstructions.PdfGenerator
{
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Shapes.Charts;
    public class ChartCreator : IDocumentCreator
    {
        private double[] values;
        private string[] xAxesValues;

        public ChartCreator(double[] values, string[] xAxesValues)
        {
            this.values = values;
            this.xAxesValues = xAxesValues;
        }

        public Document CreateDocument()
        {
            Document document = new Document();
            DefineStyles(document);

            Section section = document.AddSection();
            section.PageSetup.LeftMargin = Unit.FromCentimeter(0.5);

            section.AddParagraph("And One Constructions Inc.", "Heading1");
            section.AddParagraph("Projects By Month", "Heading2");

            Chart chart = new Chart();
            chart.Left = 0;
            chart.Width = Unit.FromCentimeter(20);
            chart.Height = Unit.FromCentimeter(12);

            var series = chart.SeriesCollection.AddSeries();
            series.ChartType = ChartType.Line;
            series.DataLabel.Type = DataLabelType.Value;
            series.Add(this.values);

            XSeries xseries = chart.XValues.AddXSeries();
            xseries.Add(this.xAxesValues);

            chart.XAxis.MajorTickMark = TickMarkType.Outside;
            chart.XAxis.Title.Caption = "Months";

            chart.YAxis.MajorTickMark = TickMarkType.Outside;
            chart.YAxis.HasMajorGridlines = true;

            chart.PlotArea.LineFormat.Color = Colors.DarkGray;
            chart.PlotArea.LineFormat.Width = 1;

            document.LastSection.Add(chart);

            return document;
        }

        public void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

        }
    }
}
