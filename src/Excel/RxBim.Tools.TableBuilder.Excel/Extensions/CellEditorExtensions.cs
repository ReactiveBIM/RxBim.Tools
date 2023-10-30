namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Extensions for <see cref="ICellEditor"/>
/// </summary>
public static class CellEditorExtensions
{
    /// <summary>
    /// Set string formula in the value
    /// </summary>
    /// <param name="editor"><see cref="ICellEditor"/></param>
    /// <param name="formula">String formula</param>
    public static ICellEditor SetFormula(this ICellEditor editor, string formula)
    {
        editor.SetContent(new StringFormulaCellContent(formula));
        return editor;
    }
}