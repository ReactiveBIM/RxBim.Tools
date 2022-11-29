# Google Sheet Table Converter
Converts a Google Sheet workbook to an `Table` from `RxBim.Tools.TableBuilder`.
## Examples
```c#
var converter = container.GetRequiredService<IFromGoogleSheetTableConverter>>();
var sheetService = GetSomeSheetService();
var spreadsheet = GetSomeSpreadsheet(sheetService);
var workbook = converter.Convert(spreadsheet, new FromGoogleSheetConverterParameters(sheetService));
```
See sample project `RxBim.Tools.TableBuilder.GoogleSheet.Sample`