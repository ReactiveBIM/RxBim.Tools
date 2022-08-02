namespace RxBim.Tools.Autocad.Sample.Services
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.Geometry;

    /// <inheritdoc />
    public class CircleService : ICircleService
    {
        private readonly Editor _editor;
        private readonly Database _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleService"/> class.
        /// </summary>
        /// <param name="editor"><see cref="Editor"/> instance.</param>
        /// <param name="database"><see cref="Database"/> instance.</param>
        public CircleService(Editor editor, Database database)
        {
            _editor = editor;
            _database = database;
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
            Database database,
            Point3d center,
            double radius,
            Transaction transaction,
            int colorIndex)
        {
            var circle = new Circle(center, Vector3d.ZAxis, radius);
            circle.ColorIndex = colorIndex;
            var blockTableRecord = transaction.GetObjectAs<BlockTableRecord>(database.CurrentSpaceId, true);
            var circleId = blockTableRecord.AppendEntity(circle);
            transaction.AddNewlyCreatedDBObject(circle, true);
            return circleId;
        }

        /// <inheritdoc />
        public ObjectId AddCircle(Point3d center, double radius, int colorIndex)
        {
            var circle = new Circle(center, Vector3d.ZAxis, radius);
            circle.ColorIndex = colorIndex;
            var transaction = _database.TransactionManager.TopTransaction;
            var blockTableRecord = transaction.GetObjectAs<BlockTableRecord>(_database.CurrentSpaceId, true);
            var circleId = blockTableRecord.AppendEntity(circle);
            transaction.AddNewlyCreatedDBObject(circle, true);
            return circleId;
        }
    }
}