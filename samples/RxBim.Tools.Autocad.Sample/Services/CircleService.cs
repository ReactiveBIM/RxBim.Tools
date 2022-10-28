namespace RxBim.Tools.Autocad.Sample.Services
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.Geometry;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class CircleService : ICircleService
    {
        private readonly Editor _editor;
        private readonly ITransactionContextService<IDatabaseWrapper> _transactionContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleService"/> class.
        /// </summary>
        /// <param name="editor"><see cref="Editor"/> instance.</param>
        /// <param name="transactionContextService"><see cref="ITransactionContextService{T}"/> instance.</param>
        public CircleService(Editor editor, ITransactionContextService<IDatabaseWrapper> transactionContextService)
        {
            _editor = editor;
            _transactionContextService = transactionContextService;
        }

        /// <inheritdoc />
        public bool TryGetCircleParams(out double radius, out Point3d center)
        {
            radius = 0;
            center = Point3d.Origin;

            var firstPointResult = _editor.GetPoint("\nSelect first point of circle diameter: ");
            if (firstPointResult.Status != PromptStatus.OK)
                return false;

            var options = new PromptPointOptions("\nSelect second point of circle diameter: ")
            {
                UseBasePoint = true,
                BasePoint = firstPointResult.Value,
                UseDashedLine = true
            };

            var secondPointResult = _editor.GetPoint(options);
            if (secondPointResult.Status != PromptStatus.OK)
                return false;

            var firstPoint = firstPointResult.Value.TransformFromUcsToWcs();
            var secondPoint = secondPointResult.Value.TransformFromUcsToWcs();

            radius = firstPoint.DistanceTo(secondPoint) / 2;
            center = firstPoint.GetMiddlePoint(secondPoint);
            return true;
        }

        /// <inheritdoc />
        public ObjectId AddCircle(
            ITransactionContextWrapper context,
            ITransactionWrapper transactionWrapper,
            Point3d center,
            double radius,
            int colorIndex)
        {
            var circle = new Circle(center, Vector3d.ZAxis, radius);
            circle.ColorIndex = colorIndex;
            return transactionWrapper.AppendToCurrentSpace(context, circle);
        }
    }
}