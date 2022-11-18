namespace RxBim.Tools.TableBuilder;

using Google.Apis.Sheets.v4;

/// <summary>
/// Contains <see cref="IFromGoogleSheetTableConverter"/> parameters.
/// </summary>
public class FromGoogleSheetConverterParameters
{
    /// <summary>
    /// Initializes an instance of the <see cref="FromGoogleSheetConverterParameters"/>
    /// </summary>
    /// <param name="sheetsService">The Sheets Service.</param>
    public FromGoogleSheetConverterParameters(SheetsService sheetsService)
    {
        SheetsService = sheetsService;
    }

    /// <summary>
    /// The worksheet name.
    /// </summary>
    public string? SheetName { get; set; }

    /// <summary>
    /// The Sheets Service.
    /// </summary>
    public SheetsService SheetsService { get; }
}