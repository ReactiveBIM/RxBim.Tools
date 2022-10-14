namespace RxBim.Tools.Revit.Helpers
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// Options for loading a family into a document.
    /// </summary>
    public class FamilyLoadOptions : IFamilyLoadOptions
    {
        private readonly bool _isOverWrite;

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyLoadOptions"/> class.
        /// </summary>
        /// <param name="isOverWrite">Overwrite parameter values.</param>
        public FamilyLoadOptions(bool isOverWrite = true)
        {
            _isOverWrite = isOverWrite;
        }

        /// <inheritdoc/>
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = _isOverWrite;
            return true;
        }

        /// <inheritdoc/>
        public bool OnSharedFamilyFound(
            Family sharedFamily,
            bool familyInUse,
            out FamilySource source,
            out bool overwriteParameterValues)
        {
            source = FamilySource.Family;
            overwriteParameterValues = true;
            return true;
        }
    }
}
