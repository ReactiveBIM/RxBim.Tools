namespace RxBim.Tools.TableBuilder.Excel.Sample
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using ClosedXML.Excel;
    using Di;

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
            var isCreateTable = args.Contains("create");

            // Create a simple container and register the table converter.
            var container = CreateContainer();

            // Getting a converter service
            var converterFrom = container.GetRequiredService<IFromExcelTableConverter>();
            var converterTo = container.GetRequiredService<IExcelTableConverter>();

            // Create or convert from an example table
            var table = isCreateTable
                ? CreateTable()
                : ConvertFromExcel(converterFrom);

            var excelFile = ConvertToExcel(converterTo, table);

            // Open Excel file
            Process.Start(excelFile);
        }

        private static IContainer CreateContainer()
        {
            var container = new SimpleInjectorContainer();
            container.AddExcelTableBuilder();
            return container;
        }

        private static string ConvertToExcel(IExcelTableConverter converterTo, Table table)
        {
            var parameters = new ExcelTableConverterParameters();

            // Convert the table to an Excel workbook
            var workbook = converterTo.Convert(table, parameters);

            // Save Excel file
            var excelFile = Save(workbook);

            return excelFile;
        }

        private static Table ConvertFromExcel(IFromExcelTableConverter converterFrom)
        {
            var parameters = new FromExcelConverterParameters();

            // Load Excel workbook
            using var workbook = Load("sample1.xlsx");

            // Convert the Excel workbook to an table 
            var table = converterFrom.Convert(workbook, parameters);

            return table;
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

        private static IXLWorkbook Load(string fileName)
        {
            var standardIfcFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                "excel",
                fileName);
            using var stream = File.OpenRead(standardIfcFilePath);
            var workbook = new XLWorkbook(stream);
            return workbook;
        }
    }
}