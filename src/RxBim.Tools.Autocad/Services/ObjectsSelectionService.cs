namespace RxBim.Tools.Autocad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.Runtime;
    using CSharpFunctionalExtensions;
    using JetBrains.Annotations;
    using AcRtException = Autodesk.AutoCAD.Runtime.Exception;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class ObjectsSelectionService : IObjectsSelectionService
    {
        private readonly Editor _editor;
        private readonly PromptSelectionOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsSelectionService"/> class.
        /// </summary>
        /// <param name="editor">Editor</param>
        public ObjectsSelectionService(Editor editor)
        {
            _editor = editor;
            _options = new PromptSelectionOptions();
            _options.KeywordInput += (_, e) => throw new AcRtException(ErrorStatus.OK, e.Input);
        }

        /// <inheritdoc />
        public Func<ObjectId, bool> CanBeSelected { get; set; } = _ => true;

        /// <inheritdoc />
        public Result<IObjectsSelectionResult> RunSelection()
        {
            var selectionResult = _editor.SelectImplied();
            if (selectionResult.Status == PromptStatus.OK)
            {
                var trueObjIds = selectionResult.Value.GetObjectIds().Where(CanBeSelected).ToArray();
                _editor.SetImpliedSelection(trueObjIds);
            }

            using (new SelectionAddedFilter(_editor, CanBeSelected))
            {
                try
                {
                    selectionResult = _editor.GetSelection(_options);
                }
                catch (AcRtException e)
                {
                    if (e.ErrorStatus == ErrorStatus.OK)
                    {
                        return new ObjectsSelectionResult { IsKeyword = true, Keyword = e.Message };
                    }
                }
            }

            if (selectionResult.Status == PromptStatus.OK && selectionResult.Value.Count > 0)
            {
                return new ObjectsSelectionResult
                {
                    IsKeyword = false,
                    SelectedObjects = selectionResult.Value.GetObjectIds()
                };
            }

            return Result.Failure<IObjectsSelectionResult>("No objects selected.");
        }

        /// <inheritdoc />
        public void SetMessageAndKeywords(string message, Dictionary<string, string>? keywordGlobalAndLocalNames = null)
        {
            _options.Keywords.Clear();
            _options.MessageForAdding = message;

            if (keywordGlobalAndLocalNames is not { Count: > 0 })
                return;

            foreach (var globalAndLocalName in keywordGlobalAndLocalNames)
            {
                _options.Keywords.Add(globalAndLocalName.Key, globalAndLocalName.Value);
            }

            _options.MessageForAdding += _options.Keywords.GetDisplayString(true);
        }
    }
}