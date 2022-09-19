﻿namespace RxBim.Tools.Revit
{
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;

    /// <inheritdoc/>
    internal class ElementSelectionFilter : ISelectionFilter
    {
        private readonly Predicate<Element>? _filterElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementSelectionFilter"/> class.
        /// </summary>
        /// <param name="filterElement">Filter for selecting elements.</param>
        public ElementSelectionFilter(Predicate<Element>? filterElement = null)
        {
            _filterElement = filterElement;
        }

        /// <inheritdoc/>
        public bool AllowElement(Element elem)
        {
            return _filterElement?.Invoke(elem) ?? true;
        }

        /// <inheritdoc/>
        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
