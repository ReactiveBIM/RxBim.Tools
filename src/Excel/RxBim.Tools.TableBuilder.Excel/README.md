TODO rewrite docs!

# Excel Table Converter
Converts a `Table` from `RxBim.Tools.TableBuilder` to an Excel workbook. And converts it back.
## Examples
```c#
var converter = container.GetRequiredService<IFromExcelTableConverter>>();
var table = new TableBuilder();
var workbook = converter.Serialize(table, new ExcelTableSerializerParameters());
```
See sample project `RxBim.Tools.Serializer.Excel.Sample`