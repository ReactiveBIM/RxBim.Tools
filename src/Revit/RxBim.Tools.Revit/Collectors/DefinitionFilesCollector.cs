namespace RxBim.Tools.Revit;

using System;
using System.Collections.Generic;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

/// <inheritdoc />
public class DefinitionFilesCollector : IDefinitionFilesCollector
{
    private readonly UIApplication _uiApplication;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SharedParameterService"/> class.
    /// </summary>
    /// <param name="uiApplication"><see cref="UIApplication"/></param>
    public DefinitionFilesCollector(UIApplication uiApplication)
    {
        _uiApplication = uiApplication;
    }
    
    private Document Document => _uiApplication.ActiveUIDocument.Document;
    
    /// <inheritdoc />
    public IDefinitionFileWrapper GetDefinitionFile(
        IDocumentWrapper? document = null)
    {
        var doc = document?.Unwrap<Document>() ?? Document;
        var sharedParameterFilename = doc.Application.SharedParametersFilename;

        if (string.IsNullOrWhiteSpace(sharedParameterFilename))
        {
            throw new ArgumentNullException(
                nameof(doc.Application.SharedParametersFilename), "Not set definition file.");
        }

        return File.Exists(sharedParameterFilename)
            ? doc.Application.OpenSharedParameterFile().Wrap()
            : throw new FileNotFoundException(
                $"Not found definition file {sharedParameterFilename}", sharedParameterFilename);
    }

    /// <inheritdoc />
    public IEnumerable<IDefinitionFileWrapper> GetDefinitionFiles(
        IEnumerable<string> filesSource)
    {
        var oldDefinitionFilePath = string.Empty;

        bool initialized;
        try
        {
            oldDefinitionFilePath = Document.Application.SharedParametersFilename;
            initialized = true;
        }
        catch
        {
            initialized = false;
        }

        var definitionFiles = new List<IDefinitionFileWrapper>();
        foreach (var filePath in filesSource)
        {
            try
            {
                Document.Application.SharedParametersFilename = new FileInfo(filePath).FullName;
                definitionFiles.Add(Document.Application.OpenSharedParameterFile().Wrap());
            }
            catch
            {
                // ignore
            }
        }

        if (initialized)
            Document.Application.SharedParametersFilename = oldDefinitionFilePath;

        return definitionFiles;
    }
}