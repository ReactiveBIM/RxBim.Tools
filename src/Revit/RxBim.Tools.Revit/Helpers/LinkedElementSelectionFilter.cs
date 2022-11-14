namespace RxBim.Tools.Revit
{
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;

    /// <inheritdoc />
    public class LinkedElementSelectionFilter : ISelectionFilter
    {
        private readonly Document _doc;
        private readonly Predicate<Element>? _filterElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedElementSelectionFilter"/> class.
        /// </summary>
        /// <param name="doc">Parent document.</param>
        /// <param name="filterElement">Filter for selecting elements.</param>
        public LinkedElementSelectionFilter(Document doc, Predicate<Element>? filterElement = null)
        {
            _doc = doc;
            _filterElement = filterElement;
        }

        /// <inheritdoc />
        public bool AllowElement(Element elem)
        {
            return true;
        }

        /// <inheritdoc />
        public bool AllowReference(Reference reference, XYZ position)
        {
            if (_doc.GetElement(reference) is not RevitLinkInstance linkInstance ||
                reference.LinkedElementId == ElementId.InvalidElementId)
                return false;

            var docLink = linkInstance.GetLinkDocument();
            var element = docLink.GetElement(reference.LinkedElementId);
            return _filterElement?.Invoke(element) ?? true;
        }
    }
}