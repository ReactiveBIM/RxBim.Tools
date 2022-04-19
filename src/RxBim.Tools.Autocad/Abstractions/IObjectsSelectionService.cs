namespace RxBim.Tools.Autocad
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Object selection service
    /// </summary>
    public interface IObjectsSelectionService
    {
        /// <summary>
        /// Checking an object by ID. If it returns true, the object can be selected.
        /// </summary>
        Func<ObjectId, bool> CanBeSelected { get; set; }

        /// <summary>
        /// Specifies the message and keywords in the select option.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="keywordGlobalAndLocalNames">Global and localized keyword names</param>
        void SetMessageAndKeywords(string message, Dictionary<string, string>? keywordGlobalAndLocalNames = null);

        /// <summary>
        /// Runs a selection of objects and returns the result of the selection.
        /// </summary>
        Result<IObjectsSelectionResult> RunSelection();
    }
}