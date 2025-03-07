namespace RxBim.Tools.TableBuilder;

using System;
using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Extensions for <see cref="CellEditor"/>
/// </summary>
public static class CellBuilderExtensions
{
    /// <summary>
    /// Sets the rotated text content.
    /// </summary>
    /// <param name="editor"><see cref="CellEditor"/> object.</param>
    /// <param name="text">Content text value.</param>
    /// <param name="angle">Text rotation angle.</param>
    /// <param name="adjustCellSize"><see cref="AutocadTextCellContent.AdjustCellSize"/> property value.</param>
    public static ICellEditor SetAcadTableText(
        this ICellEditor editor,
        string text,
        RotationAngle angle = RotationAngle.Degrees000,
        bool adjustCellSize = false)
    {
        var content = new AutocadTextCellContent(text)
        {
            Rotation = angle switch
            {
                RotationAngle.DegreesUnknown => 0,
                RotationAngle.Degrees000 => 0,
                RotationAngle.Degrees090 => Math.PI / 2,
                RotationAngle.Degrees180 => Math.PI,
                RotationAngle.Degrees270 => Math.PI * 3 / 2,
                _ => throw new ArgumentOutOfRangeException(nameof(angle), angle, null)
            },
            AdjustCellSize = adjustCellSize
        };
        return editor.SetContent(content);
    }
}