namespace RxBim.Tools.Autocad.Sample.Services
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;

    /// <inheritdoc />
    public class EntityService : IEntityService
    {
        private readonly Editor _editor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityService"/> class.
        /// </summary>
        /// <param name="editor"><see cref="Editor"/> instance.</param>
        public EntityService(Editor editor)
        {
            _editor = editor;
        }

        /// <inheritdoc />
        public bool GetEntity(out ObjectId entityId)
        {
            var entityResult = _editor.GetEntity("\nSelect an entity: ");
            if (entityResult.Status == PromptStatus.OK)
            {
                entityId = entityResult.ObjectId;
                return true;
            }

            entityId = ObjectId.Null;
            return false;
        }
    }
}