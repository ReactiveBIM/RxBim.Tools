namespace RxBim.Tools.Autocad.Sample.Abstractions
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Service for AutoCAD entities.
    /// </summary>
    public interface IEntityService
    {
        /// <summary>
        /// Returns true if getting the object ID was successful.
        /// </summary>
        /// <param name="entityId">Entity ID</param>.
        bool GetEntity(out ObjectId entityId);
    }
}