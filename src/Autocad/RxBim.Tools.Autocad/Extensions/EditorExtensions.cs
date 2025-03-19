namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using JetBrains.Annotations;

/// <summary>
/// Extensions for <see cref="Editor"/>.
/// </summary>
[PublicAPI]
public static class EditorExtensions
{
    /// <summary>
    /// Zooms the current view to the given extents.
    /// </summary>
    /// <param name="editor">The instance of <see cref="Editor"/>.</param>
    /// <param name="extents">Zoom area extents.</param>
    public static void Zoom(this Editor editor, Extents3d extents)
    {
        using var view = editor.GetCurrentView();
        extents.TransformBy(view.WorldToEye());
        view.Width = extents.MaxPoint.X - extents.MinPoint.X;
        view.Height = extents.MaxPoint.Y - extents.MinPoint.Y;
        view.CenterPoint = new Point2d(
            (extents.MaxPoint.X + extents.MinPoint.X) / 2.0,
            (extents.MaxPoint.Y + extents.MinPoint.Y) / 2.0);
        editor.SetCurrentView(view);
    }

    private static Matrix3d WorldToEye(this AbstractViewTableRecord view)
    {
        return view.EyeToWorld().Inverse();
    }

    private static Matrix3d EyeToWorld(this AbstractViewTableRecord view)
    {
        return
            Matrix3d.Rotation(-view.ViewTwist, view.ViewDirection, view.Target) *
            Matrix3d.Displacement(view.Target - Point3d.Origin) *
            Matrix3d.PlaneToWorld(view.ViewDirection);
    }
}