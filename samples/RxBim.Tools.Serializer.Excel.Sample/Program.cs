namespace RxBim.Tools.Serializer.Excel.Sample
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
    using Extensions;
    using Models;
    using TableBuilder.Abstractions;
    using TableBuilder.Extensions;
    using TableBuilder.Models;
    using TableBuilder.Services;

    /// <summary>
    /// Console program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            // Create a simple container and register a serializer
            var container = GetContainer();

            // Getting a serialization service
            var serializer = container.GetService<ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>>();
            var parameters = new ExcelTableSerializerParameters();

            // Create an example table
            var table = GetTable();

            // Serializing a table to Excel
            var workBook = table.Serialize(serializer, parameters);

            // Save Excel file and open
            var excelFile = Save(workBook);
            Process.Start(excelFile);
        }

        private static string Save(IXLWorkbook workBook)
        {
            var tempFile = Path.GetTempFileName();
            var excelFile = Path.ChangeExtension(tempFile, "xlsx");
            File.Move(tempFile, excelFile);
            workBook.SaveAs(excelFile);
            return excelFile;
        }

        private static Table GetTable()
        {
            List<(string Property, string Value)> values =
                Enumerable.Range(0, 10).Select(s => ($"Property-{s}", $"Value-{s}")).ToList();

            var table = new TableBuilder();
            table.AddRowsFromList(values, 0, 0, i => i.Property, i => i.Value);
            return table;
        }

        private static IContainer GetContainer()
        {
            var container = new SimpleInjectorContainer();
            container.AddExcelSerializer();
            return container;
        }
    }
}