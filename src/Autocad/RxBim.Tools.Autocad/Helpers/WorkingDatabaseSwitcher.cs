namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Class for temporarily switching the working database
    /// </summary>
    public class WorkingDatabaseSwitcher : IDisposable
    {
        /// <summary>
        /// Flag for whether to switch the database or not.
        /// </summary>
        private readonly bool _needSwitch;

        /// <summary>
        /// Source database
        /// </summary>
        private readonly Database _oldWorkingDatabase;

        /// <summary>
        /// Creating an auxiliary object for temporary database switching.
        /// </summary>
        /// <param name="tmpWorkDb">Link to the database to which we are temporarily switching</param>
        public WorkingDatabaseSwitcher(Database tmpWorkDb)
        {
            _oldWorkingDatabase = HostApplicationServices.WorkingDatabase;
            _needSwitch = !tmpWorkDb.Equals(_oldWorkingDatabase);
            if (_needSwitch)
                HostApplicationServices.WorkingDatabase = tmpWorkDb;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_needSwitch)
                HostApplicationServices.WorkingDatabase = _oldWorkingDatabase;
        }
    }
}