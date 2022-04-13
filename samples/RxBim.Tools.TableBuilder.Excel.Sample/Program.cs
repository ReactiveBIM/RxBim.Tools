namespace RxBim.Tools.TableBuilder.Excel.Sample
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
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
            var container = CreateContainer();

            // Getting a serialization service
            var serializer = container.GetRequiredService<IExcelTableSerializer>();
            var parameters = new ExcelTableSerializerParameters();

            // Create an example table
            var table = CreateTable();

            // Serializing a table to Excel
            var workBook = serializer.Serialize(table, parameters);

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

        private static Table CreateTable()
        {
            List<(string Property, string Value)> values =
                Enumerable.Range(0, 10).Select(s => ($"Property-{s}", $"Value-{s}")).ToList();

            var table = new TableBuilder();
            table.AddRowsFromList(values, 0, 0, i => i.Property, i => i.Value);
            return table;
        }

        private static IContainer CreateContainer()
        {
            var container = new SimpleInjectorContainer();
            container.AddExcelSerialization();
            return container;
        }
    }
}