namespace RxBim.Tools.TableBuilder;

using Google.Apis.Sheets.v4.Data;

/// <summary>
/// Represents an interface of a converter from an Google Sheet workbook to a  <see cref="Table"/> object.
/// </summary>
public interface IFromGoogleSheetTableConverter
    : ITableConverter<Spreadsheet, FromGoogleSheetConverterParameters, Table>
{
}