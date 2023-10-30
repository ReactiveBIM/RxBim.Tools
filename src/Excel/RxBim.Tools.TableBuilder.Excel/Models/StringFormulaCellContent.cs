namespace RxBim.Tools.TableBuilder;

using JetBrains.Annotations;

/// <summary>
/// The formula (via string) content of a cell
/// </summary>
[PublicAPI]
public class StringFormulaCellContent : ICellContent
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="formula">String formula</param>
    public StringFormulaCellContent(string formula)
    {
        Formula = formula;
    }

    /// <summary>
    /// String formula
    /// </summary>
    public string Formula { get; set; }

    /// <inheritdoc />
    public object ValueObject => Formula;
}