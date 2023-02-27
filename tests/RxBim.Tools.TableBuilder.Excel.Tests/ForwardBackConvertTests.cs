namespace RxBim.Tools.TableBuilder.Excel.Tests;

using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using Di;
using Xunit;

public class ForwardBackConvertTests : TestsBase
{
    [Theory]
    [InlineData("ApartmentsDduReport.xlsx")]
    public void ForwardBackConvertTest(string excelFileName)
    {
        // todo
        /*var excelPath = new FileInfo(Assembly.GetExecutingAssembly().Location)
            .Directory!
            .GetFiles(excelFileName)
            .First()
            .FullName;
        var fromExcelConverter = Container.GetService<IFromExcelTableConverter>();
        var excelTableConverter = Container.GetService<IExcelTableConverter>();
        var referenceWorkbook = GetXlWorkbook(excelPath);
        var createdWorkBook = new XLWorkbook();
        foreach (var workbookWorksheet in referenceWorkbook.Worksheets)
        {
            var table = fromExcelConverter.Convert(referenceWorkbook,
                new FromExcelConverterParameters() { WorksheetName = workbookWorksheet.Name });
            excelTableConverter.Convert(table,
                new ExcelTableConverterParameters()
                    { Workbook = createdWorkBook, WorksheetName = workbookWorksheet.Name });
        }

        var tempFile = Path.GetTempFileName().Replace(".tmp", ".xlsx");
        createdWorkBook.SaveAs(tempFile);

        // TODO add asserts
        
        File.Delete(tempFile);*/
    }

    private XLWorkbook GetXlWorkbook(string path)
    {
        using var fileStream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);
        var excelTable = new XLWorkbook(fileStream);
        return excelTable;
    }
}