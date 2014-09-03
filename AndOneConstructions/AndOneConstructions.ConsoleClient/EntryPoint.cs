/*
 * За връзка с SQL през Management Studio:
 * Server:      d80b5849-dd81-49dc-acdc-a396015983af.sqlserver.sequelizer.com
 * Login:     	rqhqhjzlydkhrstg
 * Password: 	6vULZxqG4R6jrzudTKdpEpKo6o5EnDiztsPaDvBqvTTGzUkFVuFfNERQSEPLATux
 *
 *
 * За връзка с MySQL през Workbench
 * Hostname 	d93ab522-6908-4034-9827-a396016feca4.mysql.sequelizer.com
 * Username 	omuysinjdtwtmbue
 * Password 	dxSZ7cUWJ7q7PYXKxv855mVAWi62K6nPRDGdbBzERbvUeHQq6qvhd2oYKqdWe3kL
 *
 *
 * За връзка към MongoDB
 * mongodb://teamlich:teamlich@ds063929.mongolab.com:63929/appharbor_f580ae0d-6ef8-4aac-b142-db0920bfddac
 */

namespace AndOneConstructions.ConsoleClient
{
    using AndOneConstructions.CommandHandler;
    using AndOneConstructions.Controller;
    using System.Globalization;
    using System.Threading;

    public class EntryPoint
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var importDataController = new ImportDataController();
            var exportDataController = new ExportDataController();
            var commandHadler = new CommandHadler(importDataController, exportDataController);

            commandHadler.Run();
        }
    }
}