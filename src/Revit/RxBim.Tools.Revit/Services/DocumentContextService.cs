namespace RxBim.Tools.Revit.Services
{
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// The service for <see cref="DocumentContextWrapper"/>.
    /// </summary>
    [UsedImplicitly]
    internal class DocumentContextService : ITransactionContextService<DocumentContextWrapper>
    {
        private readonly UIApplication _application;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentContextService"/> class.
        /// </summary>
        /// <param name="application"><see cref="UIApplication"/> instance.</param>
        public DocumentContextService(UIApplication application)
        {
            _application = application;
        }

        /// <inheritdoc />
        public DocumentContextWrapper GetDefaultContext()
        {
            return new DocumentContextWrapper(_application.ActiveUIDocument.Document);
        }
    }
}