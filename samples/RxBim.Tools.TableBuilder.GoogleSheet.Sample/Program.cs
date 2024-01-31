namespace RxBim.Tools.TableBuilder.GoogleSheet.Sample;

using System.Diagnostics;
using System.IO;
using System.Reflection;
using ClosedXML.Excel;
using Di;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

/// <summary>
/// Console application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main.
    /// </summary>
    public static void Main()
    {
        // Create a simple container and register the table converter.
        var container = CreateContainer();

        // Getting a converter services
        var fromGoogleSheetTableConverter = container.GetRequiredService<IFromGoogleSheetTableConverter>();
        var excelTableConverter = container.GetRequiredService<IExcelTableConverter>();

        var sheetService = GetSheetService();
        var sampleSpreadsheet = GetSampleSpreadsheet(sheetService);

        var fromGoogleSheetConverterParameters = new FromGoogleSheetConverterParameters(sheetService)
        {
            SheetName = "Sample"
        };
        var table = fromGoogleSheetTableConverter.Convert(sampleSpreadsheet, fromGoogleSheetConverterParameters);

        var excelTableConverterParameters = new ExcelTableConverterParameters();
        var xlWorkbook = excelTableConverter.Convert(table, excelTableConverterParameters);
        var path = Save(xlWorkbook);
        Process.Start(path);
    }

    private static string Save(IXLWorkbook workbook)
    {
        var tempFile = Path.GetTempFileName();
        var excelFile = Path.ChangeExtension(tempFile, "xlsx");
        File.Move(tempFile, excelFile);
        workbook.SaveAs(excelFile);
        return excelFile;
    }

    private static IContainer CreateContainer()
    {
        var container = new DiContainer();
        container.AddGoogleSheetTableBuilder();
        container.AddExcelTableBuilder();
        return container;
    }

    private static SheetsService GetSheetService()
    {
        var credential = GetCredential();
        var sheetService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential
        });
        return sheetService;
    }

    private static Spreadsheet GetSampleSpreadsheet(SheetsService sheetService)
    {
        var request = sheetService.Spreadsheets.Get("1a1pJwDIMidDGUyW4Bk_lCVoHKb29_ulM2WxV0FulU8s");
        return request.Execute();
    }

    private static ICredential GetCredential()
    {
        var path = Path
            .Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Credentials.json");
        return GoogleCredential
            .FromFile(path)
            .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly)
            .UnderlyingCredential;
    }
}