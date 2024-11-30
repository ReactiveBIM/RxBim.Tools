namespace RxBim.Tools.TableBuilder.Excel.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
    using Microsoft.Extensions.DependencyInjection;

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
            // Create a simple container and register the table converter.
            var container = CreateContainer();

            // Getting a converter service
            var converter = container.GetRequiredService<IExcelTableConverter>();
            var parameters = new ExcelTableConverterParameters();

            // Create an example table
            var table = CreateTable();

            // Convert the table to an Excel workbook
            var workbook = converter.Convert(table, parameters);

            // Save Excel file and open
            var excelFile = Save(workbook);
            Process.Start(excelFile);
        }

        private static string Save(IXLWorkbook workbook)
        {
            var tempFile = Path.GetTempFileName();
            var excelFile = Path.ChangeExtension(tempFile, "xlsx");
            File.Move(tempFile, excelFile);
            workbook.SaveAs(excelFile);
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

        private static IServiceProvider CreateContainer()
        {
            var container = new ServiceCollection();
            container.AddExcelTableBuilder();
            return container.BuildServiceProvider();
        }
    }
}