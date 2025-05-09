﻿namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;
using JetBrains.Annotations;

/// <summary>
/// Defines an interface of a <see cref="Table"/> converter to an Excel workbook.
/// </summary>
[PublicAPI]
public interface IExcelTableConverter
    : ITableConverter<Table, ExcelTableConverterParameters, IXLWorkbook>;