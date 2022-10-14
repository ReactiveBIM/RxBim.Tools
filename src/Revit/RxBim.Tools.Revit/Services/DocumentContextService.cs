namespace RxBim.Tools.Revit.Services
{
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// The service for <see cref="DocumentWrapper"/>.
    /// </summary>
    [UsedImplicitly]
    internal class DocumentContextService : ITransactionContextService<DocumentWrapper>
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
        public DocumentWrapper GetDefaultContext()
        {
            return new DocumentWrapper(_application.ActiveUIDocument.Document);
        }
    }
}