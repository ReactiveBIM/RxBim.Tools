# Excel Serializer
Serializes `Table` from `RxBim.Tools.TableBuilder` to Excel. And deserialize.
## Examples
```c#
var serializer = container.GetService<ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>>();
var table = new TableBuilder();
var workBook = serializer.Serialize(table, new ExcelTableSerializerParameters());
```
See sample project `RxBim.Tools.Serializer.Excel.Sample`