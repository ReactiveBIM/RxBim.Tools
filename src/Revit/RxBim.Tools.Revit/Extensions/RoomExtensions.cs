namespace RxBim.Tools.Revit
{
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Architecture;

    /// <summary>
    /// Расширения для помещений
    /// </summary>
    public static class RoomExtensions
    {
        /// <summary>
        /// Проверка помещения, что оно не размещено или не окружено или имеет избыточную площать
        /// </summary>
        /// <param name="room">Помещение</param>
        public static bool IsAreaValid(this Room room)
            => !(Math.Abs(room!.get_Parameter(BuiltInParameter.ROOM_AREA).AsDouble()) < 0.001);
    }
}
